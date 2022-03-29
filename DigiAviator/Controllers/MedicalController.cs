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
    public class MedicalController : BaseController
    {
        private readonly IMedicalService _service;
        private readonly ILogger<MedicalController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicalController(ILogger<MedicalController> logger,
            UserManager<ApplicationUser> userManager,
            IMedicalService service)
        {
            _logger = logger;
            _service = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Overview()
        {
            string userId = _userManager.GetUserId(User);

            var medical = await _service.GetMedical(userId);

            return View(medical);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MedicalAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = _userManager.GetUserId(User);

            if (await _service.AddMedical(userId, model))
            {
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        public IActionResult AddLimitation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLimitation(string id, LimitationAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _service.AddLimitationToMedical(id, model))
            {
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        public IActionResult AddFitness()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFitness(string id, FitnessTypeAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await _service.AddFitnessToMedical(id, model))
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
