using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Infrastructure.Data.Models;
using DigiAviator.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigiAviator.Core.Services
{
    public class AirportService : IAirportService
    {
        private readonly IApplicationDbRepository _repo;
        private readonly IValidationService _validationService;

        public AirportService(IApplicationDbRepository repo,
                    IValidationService validationService)
        {
            _repo = repo;
            _validationService = validationService;
        }

        public async Task<IEnumerable<AirportListViewModel>> GetAirports()
        {
            return await _repo.All<Airport>()
                .Select(a => new AirportListViewModel()
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                    IcaoIdentifier = a.IcaoIdentifier,
                    Elevation = a.Elevation
                })
                .ToListAsync();
        }

        public async Task<Airport> GetAirportById(Guid id)
        {
            var airport = await _repo.GetByIdAsync<Airport>(id);

            if (airport == null)
            {
                throw new ArgumentException("Unknown airport id.");
            }

            return airport;
        }

        public async Task<AirportDetailsViewModel> GetAirportDetails(string id)
        {
            var airport = await _repo.All<Airport>()
                .Where(a => a.Id == Guid.Parse(id))
                .Include(r => r.Runways)
                .FirstOrDefaultAsync();

            if (airport == null)
            {
                throw new ArgumentException("Unknown airport id.");
            }

            List<RunwayListViewModel> runways = new List<RunwayListViewModel>();

            foreach (var runway in airport.Runways)
            {
                runways.Add(new RunwayListViewModel
                {
                    Designation = runway.Designation,
                    TrueCourse = runway.TrueCourse,
                    MagneticCourse = runway.MagneticCourse,
                    Length = runway.Length,
                    Width = runway.Width,
                    Slope = runway.Slope,
                    TORA = runway.TORA,
                    TODA = runway.TODA,
                    ASDA = runway.ASDA,
                    LDA = runway.LDA
                });
            };

            return new AirportDetailsViewModel()
            {
                Id = airport.Id.ToString(),
                Name= airport.Name,
                IcaoIdentifier= airport.IcaoIdentifier,
                Description = airport.Description,
                Type = airport.Type,
                Elevation = airport.Elevation,
                Longitude = airport.Longitude,
                Latitude = airport.Latitude,
                Runways = runways
            };
        }

        public async Task<bool> AddAirport(AirportAddViewModel model)
        {
            bool result = false;

            var airport = new Airport
            {
                Name = model.Name,
                IcaoIdentifier = model.IcaoIdentifier,
                Description = model.Description,
                Type = model.Type,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Elevation = model.Elevation
            };

            var (isValid, validationError) = _validationService.ValidateModel(airport);

            if (!isValid)
            {
                throw new ArgumentException("Invalid airport information.");
            }

            try
            {
                await _repo.AddAsync(airport);
                await _repo.SaveChangesAsync();
                result = true;
            }
            catch (Exception)
            {
                throw new Exception("Unable to save airport.");
            }

            return result;
        }

        public async Task<bool> AddRunwayToAirport(string id, RunwayAddViewModel model)
        {
            var airport = await _repo.GetByIdAsync<Airport>(Guid.Parse(id));

            if (airport == null)
            {
                throw new ArgumentException("Unknown airport id.");
            }

            bool result = false;

            var runway = new Runway
            {
                AirportId = airport.Id,
                Designation = model.Designation,
                TrueCourse = model.TrueCourse,
                MagneticCourse = model.MagneticCourse,
                Length = model.Length,
                Width = model.Width,
                Slope = model.Slope,
                TORA = model.TORA,
                TODA = model.TODA,
                ASDA = model.ASDA,
                LDA = model.LDA
            };

            var (isValid, validationError) = _validationService.ValidateModel(runway);

            if (!isValid)
            {
                throw new ArgumentException("Invalid runway information, could not add runway to airport");
            }

            airport.Runways.Add(runway);

            try
            {
                await _repo.AddAsync(runway);
                await _repo.SaveChangesAsync();
                result = true;
            }
            catch (Exception)
            {
                throw new Exception("Unable to save airport.");
            }

            return result;
        }

    }
}
