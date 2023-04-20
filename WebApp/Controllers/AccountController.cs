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


            //if (await _userManager.FindByNameAsync(viewModel.Email) == null)
            //{
            //    var result = await _userManager.CreateAsync(viewModel, viewModel.Password);
            //    if (result.Succeeded)
            //        return RedirectToAction("Index");
            //}

            ModelState.AddModelError("", "A user with the same e-mail address already exists");
        }

        return View(viewModel);
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

    //public IActionResult Login()
    //{
    //    ViewData["Title"] = "Register";

    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        if(await _userService.LoginAsync(loginViewModel))
    //            return RedirectToAction("Index", "Account");

    //        ModelState.AddModelError("", "Wrong email-address or password");
    //    }

    //    return View(loginViewModel);
    //}
}
