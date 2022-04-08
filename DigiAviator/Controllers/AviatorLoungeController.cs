using Microsoft.AspNetCore.Mvc;

namespace DigiAviator.Controllers
{
    public class AviatorLoungeController : Controller
    {
        public IActionResult AviatorLounge()
        {
            return View();
        }
    }
}
