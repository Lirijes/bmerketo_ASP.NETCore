using Microsoft.AspNetCore.Mvc;
using WebApp.Contexts;

using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController( DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //flytta ut denna??
            //Ninjas kod för att hämta upp specifik tag/categori
            var bestCollectionProducts = _context.Products.Where(p => p.CategoryId == 4).ToList();
            var topCollectionProducts = _context.Products.Where(p => p.CategoryId == 5).ToList();

            var viewModel = new HomeIndexViewModel
            {
                BestCollection = new GridCollectionViewModel
                {
                    Title = "Best Collection",
                    Categories = new List<string> { "All", "Bag", "Dress", "Decoration", "Essentials", "Interior", "Laptop", "Mobile", "Beauty" },
                    GridItems = bestCollectionProducts.Select((product, index) => new GridCollectionItemViewModel
                    {
                        Id = product.Id.ToString(),
                        Title = product.Name,
                        Price = product.Price,
                        ImageUrl = product.ImgUrl
                    }).ToList()
                },

                TopCollection = new GridCollectionViewModel
                {
                    Title = "Top Collection",
                    GridItems = topCollectionProducts.Select((product, index) => new GridCollectionItemViewModel
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
    }
}