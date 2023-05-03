using WebApp.Contexts;
using WebApp.Models.Enteties;

namespace WebApp.Repository
{
    public class ProductCategoryRepo : Repository<ProductCategoryEntity>
    {
        public ProductCategoryRepo(DataContext context) : base(context)
        {
        }
    }
}
