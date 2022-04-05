using Microsoft.AspNetCore.Mvc;

namespace DigiAviator.Controllers
{
    public class LogbookController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public LogbookController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Instructions()
        {
            return View();
        }
    }
}
