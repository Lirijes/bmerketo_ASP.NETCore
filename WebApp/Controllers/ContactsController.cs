using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class ContactsController : Controller
{
    private readonly ContactsService _contactsService; //får ej denna att fungera 

    public IActionResult Index()
    {
        ViewData["Title"] = "Contact Us";

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactUsViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _contactsService.CreateAsync(model))
                return View();

            ModelState.AddModelError("", "Something went wrong when trying to send request");
        }
        return View();
    }
}
