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

        try
        {
            await _service.SendEmail(model);
        }
        catch (Exception ex)
        {
            TempData[MessageConstant.ErrorMessage] = "An error has occured while processing your request. Please try again.";
        }

        TempData[MessageConstant.SuccessMessage] = "Inquiry sent successfully";

        return RedirectToAction("Overview");
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
