using DigiAviator.Core.Constants;
using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using DigiAviator.Infrastructure.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                TempData[MessageConstant.ErrorMessage] = "Please create your logbook first!";
                return RedirectToAction(nameof(Add));
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
                TempData[MessageConstant.SuccessMessage] = "You already have a logbook!";
                return RedirectToAction(nameof(Overview));
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

            try
            {
                await _service.AddLogbook(userId, model);
                TempData[MessageConstant.SuccessMessage] = "Logbook added successfully";
            }
            catch (Exception ex)
            {
                TempData[MessageConstant.ErrorMessage] = "An error has occured while creating your logbook. Please try again.";
                return View(model);
            }

            return RedirectToAction(nameof(Overview));            
        }

        public async Task<IActionResult> Edit(string id)
        {
            //CHECK FOR LICENSE AND CACHE THE VALUE FOR 1 MINUTE//
            LogbookAddViewModel logbookToEdit;

            logbookToEdit = _memoryCache.Get<LogbookAddViewModel>("logbookToEdit_" + id);

            if (logbookToEdit == null)
            {
                logbookToEdit = await _service.GetLogbookForEdit(id);
                _memoryCache.Set("licenseToEdit_" + id, logbookToEdit, TimeSpan.FromMinutes(1));
            }

            return View(logbookToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LogbookAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = _userManager.GetUserId(User);

            if (await _service.UpdateLogbook(userId, model))
            {
                _memoryCache.Remove("license_" + _userManager.GetUserId(User));
                TempData[MessageConstant.SuccessMessage] = "Logbook updated successfully";
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Could not update logbook";
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

            try
            {
                await _service.AddFlightToLogbook(id, model);
                _memoryCache.Remove("logbook_" + _userManager.GetUserId(User));
            }
            catch (Exception ex)
            {
                TempData[MessageConstant.ErrorMessage] = "An error has occured while adding your flight. Please try again.";
                return View(model);
            }

            TempData[MessageConstant.SuccessMessage] = "Flight added successfully";

            return RedirectToAction(nameof(Overview));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFlight(string id)
        {
            try
            {
                await _service.DeleteFlight(id);
                _memoryCache.Remove("logbook_" + _userManager.GetUserId(User));
            }
            catch (Exception ex)
            {
                TempData[MessageConstant.ErrorMessage] = "An error has occured while deleting your flight. Please try again.";
            }

            TempData[MessageConstant.SuccessMessage] = "Flight deleted successfully";

            return RedirectToAction(nameof(Overview));
        }
    }
}
