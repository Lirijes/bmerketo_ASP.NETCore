using WebApp.Models.Enteties;

namespace WebApp.ViewModels
{
    public class ProductsIndexViewModel
    {
        public string Title { get; set; } = "Products";
        public  IEnumerable<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    }
}
