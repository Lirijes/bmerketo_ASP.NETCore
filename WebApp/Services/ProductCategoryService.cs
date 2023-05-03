using WebApp.Models.Enteties;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Services
{
    public class ProductCategoryService
    {
        private readonly ProductCategoryRepo _categoryRepo;

        public ProductCategoryService(ProductCategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<ProductCategoryEntity> GetOrCreateAsync(ProductCategoryModel model)
        {
            var categoryEntity = await _categoryRepo.GetAsync(x => x.Id == model.Value);
            categoryEntity ??= await _categoryRepo.AddAsync(new ProductCategoryEntity { CategoryName = model.Name });
            return categoryEntity;
        }

        public async Task<IEnumerable<ProductCategoryModel>> GetCategoriesAsync()
        {
            var items = await _categoryRepo.GetAllAsync();
            var categories = new List<ProductCategoryModel>();

            foreach (var item in items)
                categories.Add(new ProductCategoryModel { Name = item.CategoryName, Value = item.Id });

            return categories;
        }
    }
}
