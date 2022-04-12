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
    public class MedicalController : BaseController
    {
        private readonly IMedicalService _service;
        private readonly ILogger<MedicalController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _memoryCache;

        public MedicalController(ILogger<MedicalController> logger,
            UserManager<ApplicationUser> userManager,
            IMedicalService service,
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

            //CHECK FOR MEDICAL AND CACHE THE VALUE FOR 1 MINUTE//
            string? hasMedical;
            hasMedical = _memoryCache.Get<string>("hasmedical_" + userId);

            if (hasMedical == null)
            {
                bool cachedMedical = await _service.HasMedical(userId);

                hasMedical = cachedMedical.ToString().ToUpper();

                _memoryCache.Set("hasmedical_" + userId, hasMedical, TimeSpan.FromMinutes(1));
            }

            //REDIRECT IF USER DOESN'T HAVE A MEDICAL//
            if (hasMedical == "FALSE")
            {
                return RedirectToAction("Add");
            }

            //OBTAIN THE USER MEDICAL AND CACHE IT FOR 20 SECONDS//
            MedicalViewModel medical;

            medical = _memoryCache.Get<MedicalViewModel>("medical_" + userId);

            if (medical == null)
            {
                medical = await _service.GetMedical(userId);

                _memoryCache.Set("medical_" + userId, medical, TimeSpan.FromSeconds(20));
            };

            return View(medical);
        }

        public async Task<IActionResult> Add()
        {
            string userId = _userManager.GetUserId(User);

            //CHECK FOR MEDICAL AND CACHE THE VALUE FOR 1 MINUTE//
            string? hasMedical;
            hasMedical = _memoryCache.Get<string>("hasmedical_" + userId);

            if (hasMedical == null)
            {
                bool cachedMedical = await _service.HasMedical(userId);

                hasMedical = cachedMedical.ToString().ToUpper();

                _memoryCache.Set("hasmedical_" + userId, hasMedical, TimeSpan.FromMinutes(1));
            }

            //REDIRECT IF USER DOESN'T HAVE A MEDICAL//
            if (hasMedical == "TRUE")
			{
                return RedirectToAction("Overview");
            }

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

        public async Task<IActionResult> Edit(string id)
        {
            MedicalAddViewModel medicalToEdit;

            medicalToEdit = _memoryCache.Get<MedicalAddViewModel>("medicalToEdit_" + id);

            if (medicalToEdit == null)
            {
                medicalToEdit = await _service.GetMedicalForEdit(id);
                _memoryCache.Set("medicalToEdit_" + id, medicalToEdit, TimeSpan.FromMinutes(1));
            }

            return View(medicalToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MedicalAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = _userManager.GetUserId(User);

            if (await _service.UpdateMedical(userId, model))
            {
                _memoryCache.Remove("medical_" + _userManager.GetUserId(User));
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
                _memoryCache.Remove("medical_" + _userManager.GetUserId(User));
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLimitation(string id)
        {

            if (await _service.DeleteLimitation(id))
            {
                _memoryCache.Remove("medical_" + _userManager.GetUserId(User));
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View("Overview");
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
                _memoryCache.Remove("medical_" + _userManager.GetUserId(User));
                return RedirectToAction("Overview");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFitness(string id)
        {

            if (await _service.DeleteFitness(id))
            {
                _memoryCache.Remove("medical_" + _userManager.GetUserId(User));
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
