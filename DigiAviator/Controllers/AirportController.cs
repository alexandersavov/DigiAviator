using DigiAviator.Core.Constants;
using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DigiAviator.Controllers
{
    public class AirportController : BaseController
    {
        private readonly IAirportService _service;
        private readonly ILogger<AirportController> _logger;

        public AirportController(ILogger<AirportController> logger,
            IAirportService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Overview()
        {
            var airports = await _service.GetAirports();

            return View(airports);
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

        public async Task<IActionResult> Details(string id)
        {
            var airport = await _service.GetAirportDetails(id);

            return View(airport);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
