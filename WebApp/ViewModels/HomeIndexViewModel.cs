using WebApp.Models.Enteties;

namespace WebApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public string Title { get; set; } = "Home";
        public GridCollectionViewModel BestCollection { get; set; } = null!;
        public GridCollectionViewModel SummerCollection { get; set; } = null!;
        public IEnumerable<ProductEntity> Products { get; set; } = null!;
    }
}