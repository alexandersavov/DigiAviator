using DigiAviator.Core.Contracts;
using DigiAviator.Core.Constants;
using DigiAviator.Core.Models;
using DigiAviator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using DigiAviator.Infrastructure.Data.Models.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace DigiAviator.Controllers
{
    public class LogbookController : BaseController
    {
        private readonly ILogger<LogbookController> _logger;
        private readonly ILogbookService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _memoryCache;

        public LogbookController(ILogger<LogbookController> logger,
            UserManager<ApplicationUser> userManager,
            ILogbookService service,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _userManager = userManager;
            _service = service;
            _memoryCache = memoryCache;
        }

        public IActionResult Instructions()
        {
            return View();
        }

        public async Task<IActionResult> Overview()
        {
            string userId = _userManager.GetUserId(User);

            //CHECK FOR LOGBOOK AND CACHE THE VALUE FOR 1 MINUTE//
            string? hasLogbook;
            hasLogbook = _memoryCache.Get<string>("haslogbook_" + userId);

            if (hasLogbook == null)
            {
                bool cachedLogbook = await _service.HasLogbook(userId);

                hasLogbook = cachedLogbook.ToString().ToUpper();

                _memoryCache.Set("haslogbook_" + userId, hasLogbook, TimeSpan.FromMinutes(1));
            }

            //REDIRECT IF USER DOESN'T HAVE A LOGBOOK//
            if (hasLogbook == "FALSE")
            {
                return RedirectToAction("Add");
            }

            //OBTAIN THE USER LOGBOOK AND CACHE IT FOR 20 SECONDS//
            LogbookViewModel logbook;

            logbook = _memoryCache.Get<LogbookViewModel>("logbook_" + userId);

            if (logbook == null)
            {
                logbook = await _service.GetLogbook(userId);

                _memoryCache.Set("logbook_" + userId, logbook, TimeSpan.FromSeconds(20));
            };

            return View(logbook);
        }

        public async Task<IActionResult> Add()
        {
            string userId = _userManager.GetUserId(User);

            //CHECK FOR LOGBOOK AND CACHE THE VALUE FOR 1 MINUTE//
            string? hasLogbook;
            hasLogbook = _memoryCache.Get<string>("haslogbook_" + userId);

            if (hasLogbook == null)
            {
                bool cachedLogbook = await _service.HasLogbook(userId);

                hasLogbook = cachedLogbook.ToString().ToUpper();

                _memoryCache.Set("haslogbook_" + userId, hasLogbook, TimeSpan.FromMinutes(1));
            }

            //REDIRECT IF USER HAS A LOGBOOK//
            if (hasLogbook == "TRUE")
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
                _memoryCache.Remove("logbook_" + _userManager.GetUserId(User));
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
                _memoryCache.Remove("logbook_" + _userManager.GetUserId(User));
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
