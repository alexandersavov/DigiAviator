using Microsoft.AspNetCore.Mvc;

namespace DigiAviator.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
