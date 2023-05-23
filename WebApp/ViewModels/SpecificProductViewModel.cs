using WebApp.Models.Enteties;

namespace WebApp.ViewModels
{
    public class SpecificProductViewModel
    {
        public ProductEntity? Product { get; set; }
        public GridCollectionViewModel RelatedProducts { get; set; } = null!;
        public GridCollectionViewModel FeaturedProducts { get; set; } = null!;
    }
}
