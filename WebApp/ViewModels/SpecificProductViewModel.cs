using WebApp.Models.Enteties;

namespace WebApp.ViewModels
{
    public class SpecificProductViewModel
    {
        public ProductEntity? Product { get; set; }
        public List<GridCollectionItemViewModel> RelatedProduct { get; set; } = null!;
    }
}
