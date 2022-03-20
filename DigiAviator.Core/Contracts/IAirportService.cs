using DigiAviator.Core.Models;
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
    }
}
