using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Account";

        return View();
    }

    public IActionResult Register()
    {
        ViewData["Title"] = "Register";

        return View();
    }

    //[HttpPost]
    //public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        if (await _userService.DoUserExists(x => x.Email == registerViewModel.Email))
    //        {
    //            ModelState.AddModelError("", "The email-address is already registered");
    //        }
    //        else
    //        {
    //            if (await _userService.RegisterAsync(registerViewModel))
    //                return RedirectToAction("Login", "Account"); //vi omdirigerar oss till HomeControllern
    //            else
    //                ModelState.AddModelError("", "Something went wrong when trying to create profile");
    //        }
    //    }

    //    return View(registerViewModel);
    //}

    public IActionResult Login()
    {
        ViewData["Title"] = "Register";

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            if(await _userService.LoginAsync(loginViewModel))
                return RedirectToAction("Index", "Account");

            ModelState.AddModelError("", "Wrong email-address or password");
        }
        
        return View(loginViewModel);
    }
}
