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
    public class LicenseController : BaseController
    {
        private readonly ILicenseService _service;
        private readonly ILogger<LicenseController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _memoryCache;

        public LicenseController(ILogger<LicenseController> logger,
            UserManager<ApplicationUser> userManager,
            ILicenseService service, 
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Overview()
        {
            string userId = _userManager.GetUserId(User);

            //CHECK FOR LICENSE AND CACHE THE VALUE FOR 1 MINUTE//
            string? hasLicense;
            hasLicense = _memoryCache.Get<string>("haslicense_" + userId);

            if (hasLicense == null)
            {
                bool cachedLicense = await _service.HasLicense(userId);

                hasLicense = cachedLicense.ToString().ToUpper();

                _memoryCache.Set("haslicense_" + userId, hasLicense, TimeSpan.FromMinutes(1));
            }

            //REDIRECT IF USER DOESN'T HAVE A LICENSE//
            if (hasLicense == "FALSE")
            {
                return RedirectToAction("Add");
            }

            //OBTAIN THE USER LICENSE AND CACHE IT FOR 20 SECONDS//
            LicenseViewModel license;

            license = _memoryCache.Get<LicenseViewModel>("license_" + userId);

            if (license == null)
            {
                license = await _service.GetLicense(userId);

                _memoryCache.Set("license_" + userId, license, TimeSpan.FromSeconds(20));
            };

            return View(license);
        }

        public async Task<IActionResult> Add()
        {
            var userId = _userManager.GetUserId(User);

            //CHECK FOR LICENSE AND CACHE THE VALUE FOR 1 MINUTE//
            string? hasLicense;
            hasLicense = _memoryCache.Get<string>("haslicense_" + userId);

            if (hasLicense == null)
            {
                bool cachedLicense = await _service.HasLicense(userId);

                hasLicense = cachedLicense.ToString().ToUpper();

                _memoryCache.Set("haslicense_" + userId, hasLicense, TimeSpan.FromMinutes(1));
            }

            //REDIRECT IF USER HAS A LICENSE//
            if (hasLicense == "TRUE")
            {
                return RedirectToAction("Overview");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LicenseAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = _userManager.GetUserId(User);

            if (await _service.AddLicense(userId, model))
            {
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            //CHECK FOR LICENSE AND CACHE THE VALUE FOR 1 MINUTE//
            LicenseAddViewModel licenseToEdit;

            licenseToEdit = _memoryCache.Get<LicenseAddViewModel>("licenseToEdit_" + id);

            if (licenseToEdit == null)
            {
                licenseToEdit = await _service.GetLicenseForEdit(id);
                _memoryCache.Set("licenseToEdit_" + id, licenseToEdit, TimeSpan.FromMinutes(1));
            }

            return View(licenseToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LicenseAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = _userManager.GetUserId(User);

            if (await _service.UpdateLicense(userId, model))
            {
                _memoryCache.Remove("license_" + _userManager.GetUserId(User));
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        public IActionResult AddLanguage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLanguage(string id, LanguageAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _service.AddLanguageToLicense(id, model))
            {
                _memoryCache.Remove("license_" + _userManager.GetUserId(User));
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLanguage(string id)
        {

            if (await _service.DeleteLanguage(id))
            {
                _memoryCache.Remove("license_" + _userManager.GetUserId(User));
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View("Overview");
        }

        public IActionResult AddRating()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(string id, RatingAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _service.AddRatingToLicense(id, model))
            {
                _memoryCache.Remove("license_" + _userManager.GetUserId(User));
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRating(string id)
        {

            if (await _service.DeleteRating(id))
            {
                _memoryCache.Remove("license_" + _userManager.GetUserId(User));
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View("Overview");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
