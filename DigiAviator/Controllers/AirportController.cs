using DigiAviator.Core.Constants;
using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace DigiAviator.Controllers
{
    public class AirportController : BaseController
    {
        private readonly IAirportService _service;
        private readonly ILogger<AirportController> _logger;
        private readonly IMemoryCache _memoryCache;

        public AirportController(ILogger<AirportController> logger,
            IAirportService service,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _service = service;
            _memoryCache = memoryCache;
        }

        //GET ALL AIRPORTS//
        public async Task<IActionResult> Overview()
        {
            IEnumerable<AirportListViewModel> airports;

            //CHECK FOR AIRPORTS AND CACHE THE VALUE FOR 20 SECONDS//
            airports = _memoryCache.Get<List<AirportListViewModel>>("airports");

            if (airports == null)
            {
                airports = await _service.GetAirports();

                _memoryCache.Set("airports", airports, TimeSpan.FromSeconds(20));
            }

            return View(airports);
        }

        //GET AIRPORT DETAILS//
        public async Task<IActionResult> Details(string id)
        {
            AirportDetailsViewModel airport;

            //CHECK FOR AIRPORTS DETAILS AND CACHE THE VALUE FOR 20 SECONDS//
            airport = _memoryCache.Get<AirportDetailsViewModel>("airport_" + id);

            if (airport == null)
            {
                airport = await _service.GetAirportDetails(id);

                _memoryCache.Set("airport_" + id, airport, TimeSpan.FromSeconds(20));
            };

            return View(airport);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
