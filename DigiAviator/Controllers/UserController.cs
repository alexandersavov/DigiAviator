using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DigiAviator.Core.Constants;
using DigiAviator.Core.Contracts;
using DigiAviator.Infrastructure.Data.Models.Identity;

namespace DigiAviator.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<ApplicationUser> _userManager;

        //private readonly IUserService _service;

        public UserController(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole()
        {
            //await _roleManager.CreateAsync(new IdentityRole()
            //{
            //    Name = "Administrator"
            //});

            return Ok();
        }
    }
}
