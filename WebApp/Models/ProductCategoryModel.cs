using WebApp.Models.Enteties;

namespace WebApp.Models
{
    public class ProductCategoryModel
    {
        public int Value { get; set; }
        public string Name { get; set; } = null!;


        public static implicit operator ProductCategoryEntity(ProductCategoryModel productCategoryModel) //viewmodel ska kunna bli en productentity
        {
            return new ProductCategoryEntity
            {
                Id = productCategoryModel.Value,
                CategoryName = productCategoryModel.Name
            };
        }
    }
}
