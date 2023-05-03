using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ProductRegistrationViewModel
    {
        public ProductRegisterModel Form { get; set; } = new ProductRegisterModel();
        //public IEnumerable<ProductCategoryModel> ProductCategories { get; set; } = new List<ProductCategoryModel>();
    }
}
