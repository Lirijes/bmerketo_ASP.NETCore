using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Contexts;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class AccountController : Controller
{
    private readonly UserService _userService;
    private readonly DataContext _context;

    public AccountController(UserService userService, DataContext context)
    {
        _userService = userService;
        _context = context;
    }

    [Authorize]
    public IActionResult Index()
    {
        ViewData["Title"] = "Account";

        var featuredCollectionProducts = _context.Products.Where(p => p.CategoryId == 6).ToList();

        var viewModel = new HomeIndexViewModel
        {
            FeaturedCollection = new GridCollectionViewModel
            {
                Title = "Featured Products",
                Categories = null,
                GridItems = featuredCollectionProducts.Select((product, index) => new GridCollectionItemViewModel
                {
                    Id = product.Id.ToString(),
                    Title = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImgUrl
                }).ToList()
            }
        };

        return View(viewModel);
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
            if (await _userService.RegisterAsync(viewModel))
                return RedirectToAction("Login");

            ModelState.AddModelError("", "A user with the same e-mail address already exists");
        }

        return View(viewModel);
    }
}
