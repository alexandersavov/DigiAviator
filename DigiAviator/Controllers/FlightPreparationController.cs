using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigiAviator.Controllers
{
    public class FlightPreparationController : BaseController
    {
        private readonly IFlightPreparationService _service;

        public FlightPreparationController(IFlightPreparationService service)
        {
            _service = service;
        }


        public IActionResult AirspaceMap()
        {
            return View();
        }

        public IActionResult WeightAndBalance()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WeightAndBalance(WeightBalanceAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var calculation = _service.CalculateWeightBalance(model);

            return View("CalculatedWeightAndBalance", calculation);
        }
    }
}
