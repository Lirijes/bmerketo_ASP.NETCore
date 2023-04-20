using Microsoft.AspNetCore.Mvc;
using WebApp.Contexts;
using WebApp.Models.Enteties;
using WebApp.Services;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class ProductsController : Controller
{
    private readonly ProductService _productService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly DataContext _context;

    public ProductsController(ProductService productService, IWebHostEnvironment webHostEnvironment, DataContext context)
    {
        _productService = productService;
        _webHostEnvironment = webHostEnvironment;
        _context = context;
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
    public async Task<IActionResult> Register(ProductRegistrationViewModel productRegistrationViewModel, IFormFile photo)
    {
        if (ModelState.IsValid)
        {
            //if(photo == null || photo.Length == 0)
            //{
            //    return Content("File not selected");
            //}
            //var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploadedimg/products", photo.FileName);
            //using (FileStream stream = new FileStream(path, FileMode.Create))
            //{
            //    await photo.CopyToAsync(stream);
            //    stream.Close();
            //}

            //productRegistrationViewModel.ProductEntity.Picture = photo.FileName;

            //if(productRegistrationViewModel != null)
            //{
            //    var productEntity = new ProductEntity
            //    {
            //        Name = productRegistrationViewModel.ProductEntity.Name,
            //        Description = productRegistrationViewModel.ProductEntity.Description,
            //        Price = productRegistrationViewModel.ProductEntity.Price,
            //        Picture = productRegistrationViewModel.ProductEntity.Picture,
            //        ImageLocation = path
            //    };
            //    _context.Add(productEntity);
            //    await _context.SaveChangesAsync();
            //}
            //return RedirectToAction("Index");


            //if (productRegistrationViewModel != null)
            //{
            //    string folder = "uploadedimg/products";
            //    folder += productRegistrationViewModel.Picture.FileName+Guid.NewGuid().ToString();
            //    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

            //    await productRegistrationViewModel.Picture.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            //}


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

    //[HttpPost] HJÄLP ATT FIXA DETTA
    //public async Task<IActionResult> Search(ProductRegistrationViewModel productRegistrationViewModel)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        if (await _productService.GetProductAsync())
    //            return View();
    //    }
    //    return View();
    //}

    public IActionResult SpecificItem()
    {
        ViewData["Title"] = "Specifics";

        return View();
    }
}
