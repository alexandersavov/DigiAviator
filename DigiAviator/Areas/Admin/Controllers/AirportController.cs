using DigiAviator.Core.Constants;
using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace DigiAviator.Areas.Admin.Controllers
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


        public async Task<IActionResult> Overview()
        {
            IEnumerable<AirportListViewModel> airports;

            airports = _memoryCache.Get<List<AirportListViewModel>>("airports");

            if (airports == null)
            {
                airports = await _service.GetAirports();

                _memoryCache.Set("airports", airports, TimeSpan.FromSeconds(20));
            }

            return View(airports);
        }

        public async Task<IActionResult> Details(string id)
        {
            AirportDetailsViewModel airport;

            airport = _memoryCache.Get<AirportDetailsViewModel>("airport_" + id);

            if (airport == null)
            {
                airport = await _service.GetAirportDetails(id);

                _memoryCache.Set("airport_" + id, airport, TimeSpan.FromSeconds(20));
            };

            return View(airport);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AirportAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _service.AddAirport(model))
            {
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        public IActionResult AddRunway()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRunway(string id, RunwayAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _service.AddRunwayToAirport(id, model))
            {
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
