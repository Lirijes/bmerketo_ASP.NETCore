using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class ProductsController : Controller
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        var viewModel = new ProductsIndexViewModel
        {
            All = new GridCollectionViewModel
            {
                Title = "All Products",
                Categories = new List<string> { "All", "Mobile", "Computers" }
            }
        };
        return View(viewModel);
    }

    public IActionResult Register ()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(ProductRegistrationViewModel productRegistrationViewModel)
    {
        if (ModelState.IsValid)
        {
            if (await _productService.CreateAsync(productRegistrationViewModel))
                return RedirectToAction("Index", "Products");

            ModelState.AddModelError("", "Something went wrong when trying to create product");
        }
        return View();
    }

    public IActionResult Search()
    {
        ViewData["Title"] = "Search for products";

        return View();
    }

    public IActionResult SpecificItem()
    {
        ViewData["Title"] = "Specifics";

        return View();
    }
}
