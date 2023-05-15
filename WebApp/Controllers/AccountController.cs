using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    public IActionResult Index()
    {
        ViewData["Title"] = "Account";

        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            if (await _userService.LoginAsync(viewModel))
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Incorrect email address or password");
        }

        return View(viewModel);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        if (await _userService.LogoutAsync(User))
            return LocalRedirect("/");

        return RedirectToAction("Index", "Home");
        //if (_signInManager.IsSignedIn(User))
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}

        //return RedirectToAction("Index", "Login");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            if(await _userService.RegisterAsync(viewModel))
                return RedirectToAction("Login");

            ModelState.AddModelError("", "A user with the same e-mail address already exists");
        }

        return View(viewModel);
    }
}
