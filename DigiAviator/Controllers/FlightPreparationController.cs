using Microsoft.AspNetCore.Mvc;

namespace DigiAviator.Controllers
{
    public class FlightPreparationController : BaseController
    {
        public IActionResult AirspaceMap()
        {
            return View();
        }
    }
}
