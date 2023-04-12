using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Identity;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<CustomIdentityUser> _signInManager;
    private readonly UserManager<CustomIdentityUser> _userManager;

    public AccountController(SignInManager<CustomIdentityUser> signInManager, UserManager<CustomIdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    //private readonly UserService _userService;

    //public AccountController(UserService userService)
    //{
    //    _userService = userService;
    //}

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
    public async Task<IActionResult> Login(UserLoginViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, false, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Incorrect email address or password");
        }

        return View(viewModel);
    }

    public async Task<IActionResult> Logout()
    {
        if (_signInManager.IsSignedIn(User))
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("Index", "Login");
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
            if (await _userManager.FindByNameAsync(viewModel.Email) == null)
            {
                var result = await _userManager.CreateAsync(viewModel, viewModel.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Login");
            }

            ModelState.AddModelError("", "A user with the same e-mail address already exists");
        }

        return View(viewModel);
    }

    //public IActionResult Register()
    //{
    //    ViewData["Title"] = "Register";

    //    return View();
    //}

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
