using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class ProductsController : Controller
{
    private readonly ProductService _productService;
    private readonly ProductCategoryService _productCategoryService;
    private readonly DataContext _context;

    public ProductsController(ProductService productService, ProductCategoryService productCategoryService, DataContext context)
    {
        _productService = productService;
        _productCategoryService = productCategoryService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new ProductsIndexViewModel
        {
            Products = await _productService.GetAllProductsAsync()
        };

        return View(viewModel);
    }

    [Authorize(Roles = "admin")]
    public IActionResult RegisterCategory()
    {
        return View();
    }

    [Authorize(Roles = "admin")]
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

    [Authorize(Roles = "admin")]
    public IActionResult Register()
    {
        return View();
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> Register(ProductRegistrationViewModel productRegistrationViewModel)
    {
        if (ModelState.IsValid)
        {
            var product = await _productService.CreateProductAsync(productRegistrationViewModel);
            if (product != null)
            {
                if(productRegistrationViewModel.ImgUrl != null)
                    await _productService.UploadImageAsync(product, productRegistrationViewModel.ImgUrl!);
                
                return RedirectToAction("Index", "Products");
            }
        }

        ModelState.AddModelError("", "Something went wrong when trying to create product");
        return View(productRegistrationViewModel);
    }

    public IActionResult Search()
    {
        ViewData["Title"] = "Search for products";

        return View();
    }

    public async Task<IActionResult> SpecificItem(string id)
    {
        ViewData["Title"] = "Specifics";
       
        var _id = Convert.ToInt32(id); // id blivit int igen
        var relatedProducts = _context.Products.Where(p => p.CategoryId == 6).ToList();
        var featuredProducts = _context.Products.Where(p => p.CategoryId == 4).ToList();

        var item = await _productService.GetByIdAsync(_id);
        if (item != null)
        {
            var viewModel = new SpecificProductViewModel 
            { 
                Product = item,
                RelatedProducts = new GridCollectionViewModel
                {
                    Title = "Related Products",
                    Categories = null,
                    GridItems = relatedProducts.Select((product, index) => new GridCollectionItemViewModel
                    {
                        Id = product.Id.ToString(),
                        Title = product.Name,
                        Price = product.Price,
                        ImageUrl = product.ImgUrl
                    }).ToList()
                },
                FeaturedProducts = new GridCollectionViewModel
                {
                    Title = "Featured Products",
                    Categories = null,
                    GridItems = relatedProducts.Select((product, index) => new GridCollectionItemViewModel
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
        return RedirectToAction("Index", "Products");
    }
}
