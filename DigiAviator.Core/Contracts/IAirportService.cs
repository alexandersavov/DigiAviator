using DigiAviator.Core.Models;
using DigiAviator.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiAviator.Core.Contracts
{
    public interface IAirportService
    {
        Task<IEnumerable<AirportListViewModel>> GetAirports();

        Task<bool> AddAirport(AirportAddViewModel model);

        Task<Airport> GetAirportById(Guid id);

        Task<AirportDetailsViewModel> GetAirportDetails(string id);

        Task<bool> AddRunwayToAirport(string id, RunwayAddViewModel model);
    }
}
