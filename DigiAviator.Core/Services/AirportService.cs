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

        public AirportService(IApplicationDbRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<AirportListViewModel>> GetAirports()
        {
            return await _repo.All<Airport>()
                .Select(a => new AirportListViewModel()
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                    IcaoIdentifier = a.IcaoIdentifier,
                    Elevation = a.Elevation,
                    Longitude = a.Longitude,
                    Latitude = a.Latitude
                })
                .ToListAsync();
        }


        public async Task<bool> AddAirport(AirportAddViewModel model)
        {
            bool result = false;

            var airport = new Airport
            {
                Name = model.Name,
                IcaoIdentifier = model.IcaoIdentifier,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Elevation = model.Elevation
            };

            if (airport != null)
            {
                await _repo.AddAsync(airport);
                await _repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

    }
}
