using DigiAviator.Core.Contracts;
using DigiAviator.Core.Constants;
using DigiAviator.Core.Models;
using DigiAviator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using DigiAviator.Infrastructure.Data.Models.Identity;

namespace DigiAviator.Controllers
{
    public class LogbookController : BaseController
    {
        private readonly ILogger<LogbookController> _logger;
        private readonly ILogbookService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public LogbookController(ILogger<LogbookController> logger,
            UserManager<ApplicationUser> userManager,
            ILogbookService service)
        {
            _logger = logger;
            _userManager = userManager;
            _service = service;
        }

        public IActionResult Instructions()
        {
            return View();
        }

        public async Task<IActionResult> Overview()
        {
            if (!await _service.HasLogbook(_userManager.GetUserId(User)))
            {
                return RedirectToAction("Add");
            }

            string userId = _userManager.GetUserId(User);

            var logbook = await _service.GetLogbook(userId);

            return View(logbook);
        }

        [HttpPost]
        public async Task<IActionResult> Overview(string id, FlightAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _service.AddFlightToLogbook(id, model))
            {
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            if (await _service.HasLogbook(_userManager.GetUserId(User)))
            {
                return RedirectToAction("Overview");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LogbookAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = _userManager.GetUserId(User);

            if (await _service.AddLogbook(userId, model))
            {
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        public IActionResult AddFlight()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight(string id, FlightAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _service.AddFlightToLogbook(id, model))
            {
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFlight(string id)
        {

            if (await _service.DeleteFlight(id))
            {
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View("Overview");
        }
    }
}
