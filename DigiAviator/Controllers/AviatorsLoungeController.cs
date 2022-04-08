using Microsoft.AspNetCore.Mvc;

namespace DigiAviator.Controllers
{
    public class AviatorsLoungeController : Controller
    {
        public IActionResult AviatorsLounge()
        {
            return View();
        }
    }
}
