using DigiAviator.Core.Constants;
using DigiAviator.Core.Contracts;
using DigiAviator.Core.Models;
using Microsoft.AspNetCore.Mvc;

public class AboutController : Controller
{
    private readonly IEmailService _service;
    private readonly ILogger<AboutController> _logger;

    public AboutController(ILogger<AboutController> logger,
        IEmailService service)
    {
        _logger = logger;
        _service = service;
    }

    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Contact(EmailSubmitViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (await _service.SendEmail(model))
        {
            return RedirectToAction("Overview");
        }
        else
        {
            ViewData[MessageConstant.ErrorMessage] = "Възникна грешка!";
        }

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
