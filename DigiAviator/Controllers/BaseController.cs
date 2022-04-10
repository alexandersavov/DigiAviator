using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigiAviator.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
