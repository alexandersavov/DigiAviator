using DigiAviator.Core.Contracts;
using DigiAviator.Infrastructure.Data.Models.Identity;
using DigiAviator.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DigiAviator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILogbookService _logbookService;
        private readonly ILicenseService _licenseService;
        private readonly IMedicalService _medicalService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
            UserManager<ApplicationUser> userManager,
            ILogbookService logbookService,
            IMedicalService medicalService,
            ILicenseService licenseService)
        {
            _logger = logger;
            _logbookService = logbookService;
            _medicalService = medicalService;
            _licenseService = licenseService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                ViewData["LongestFlight"] = await _logbookService.GetLongestFlight(userId);
                ViewData["TotalFlightTime"] = await _logbookService.GetTotalFlightTime(userId);
                ViewData["MostFlownAircraft"] = await _logbookService.GetMostFlownAircraft(userId);
                ViewData["ValidMedical"] = await _medicalService.HasValidMedical(userId);
                ViewData["ValidLicense"] = await _licenseService.HasValidLicense(userId);
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}