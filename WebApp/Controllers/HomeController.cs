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
            //flytta ut denna 
            var bestCollectionProducts = _context.Products.Where(p => p.CategoryId == 1).ToList();

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
                }


                //här ska vi leta fram produkter med en speciell tag -service för det?

                //    BestCollection = new GridCollectionViewModel
                //    {
                //        Title = "Best Collection",
                //Categories = new List<string> { "All", "Bag", "Dress", "Decoration", "Essentials", "Interior", "Laptops", "Mobile", "Beauty" },
                //GridItems = new List<ProductRegistrationViewModel>() //ändrat från gridviewmodel till productregistrationviewmodel
                //{
                //    new GridCollectionItemViewModel { Id = "1" , Title = "Apple watch collection", Price = 10, ImageUrl = "images/placeholders/270x295.svg" },
                //    new GridCollectionItemViewModel { Id = "2" , Title = "Apple watch collection", Price = 20, ImageUrl = "images/placeholders/270x295.svg" },
                //    new GridCollectionItemViewModel { Id = "3" , Title = "Apple watch collection", Price = 30, ImageUrl = "images/placeholders/270x295.svg" },
                //    new GridCollectionItemViewModel { Id = "4" , Title = "Apple watch collection", Price = 40, ImageUrl = "images/placeholders/270x295.svg" },
                //    new GridCollectionItemViewModel { Id = "5" , Title = "Apple watch collection", Price = 50, ImageUrl = "images/placeholders/270x295.svg" },
                //    new GridCollectionItemViewModel { Id = "6" , Title = "Apple watch collection", Price = 60, ImageUrl = "images/placeholders/270x295.svg" },
                //    new GridCollectionItemViewModel { Id = "7" , Title = "Apple watch collection", Price = 70, ImageUrl = "images/placeholders/270x295.svg" },
                //    new GridCollectionItemViewModel { Id = "8" , Title = "Apple watch collection", Price = 80, ImageUrl = "images/placeholders/270x295.svg" }
                //}
                //}
            };
            return View(viewModel);
        }
    }
}