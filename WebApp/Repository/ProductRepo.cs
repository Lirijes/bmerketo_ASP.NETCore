using WebApp.Contexts;
using WebApp.Models.Enteties;

namespace WebApp.Repository
{
    public class ProductRepo : Repository<ProductEntity>
    {
        public ProductRepo(DataContext context) : base(context)
        {
        }
    }
}
