using Microsoft.AspNetCore.Mvc;

namespace DigiAviator.Controllers
{
    public class AviatorLoungeController : BaseController
    {
        public IActionResult AviatorLounge()
        {
            return View();
        }
    }
}
