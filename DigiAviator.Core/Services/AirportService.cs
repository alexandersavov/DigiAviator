using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Infrastructure.Data.Models;
using DigiAviator.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DigiAviator.Core.Services
{
    public class AirportService : IAirportService
    {
        private readonly IApplicatioDbRepository repo;

        public AirportService(IApplicatioDbRepository _repo)
        {
            repo = _repo;
        }
        public async Task<IEnumerable<AirportListViewModel>> GetAirports()
        {
            return await repo.All<Airport>()
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
                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

    }
}
