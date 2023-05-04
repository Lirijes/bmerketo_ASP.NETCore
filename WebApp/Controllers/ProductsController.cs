using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class ProductsController : Controller
{
    private readonly ProductService _productService;
    private readonly ProductCategoryService _productCategoryService;

    public ProductsController(ProductService productService, ProductCategoryService productCategoryService)
    {
        _productService = productService;
        _productCategoryService = productCategoryService;
    }

    public async Task<IActionResult> Index()
    {
        //fungerar ej 
        var products = await _productService.GetAllProductsAsync(); 

        return View(products);
    }

    public IActionResult RegisterCategory()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterCategory(ProductCategoryModel productCategoryModel)
    {
        //fungerar ej
        if (ModelState.IsValid)
        {
            await _productCategoryService.GetOrCreateAsync(productCategoryModel);
            return RedirectToAction("Index", "Products");
        }
        return View(productCategoryModel);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(ProductRegistrationViewModel productRegistrationViewModel)
    {
        if (ModelState.IsValid)
        {
            await _productService.CreateAsync(productRegistrationViewModel);
                return RedirectToAction("Index", "Products");
        }

        ModelState.AddModelError("", "Something went wrong when trying to create product");
        return View(productRegistrationViewModel);
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
