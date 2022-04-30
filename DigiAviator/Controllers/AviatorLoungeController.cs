using DigiAviator.Infrastructure.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DigiAviator.Controllers
{
    public class AviatorLoungeController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AviatorLoungeController(UserManager<ApplicationUser> userManager)
        {          
            _userManager = userManager;
        }
        public IActionResult AviatorLounge()
        {
            ViewData["User"] = _userManager.GetUserName(User);

            return View();
        }
    }
}
