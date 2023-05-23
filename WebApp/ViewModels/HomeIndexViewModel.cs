namespace WebApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public string Title { get; set; } = "Home";
        public GridCollectionViewModel BestCollection { get; set; } = null!;
        public GridCollectionViewModel TopCollection { get; set; } = null!;
        public GridCollectionViewModel FeaturedCollection { get; set; } = null!;
    }
}