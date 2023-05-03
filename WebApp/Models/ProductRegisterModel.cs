using System.ComponentModel.DataAnnotations;
using WebApp.Models.Enteties;

namespace WebApp.Models
{
    public class ProductRegisterModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Product Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Product price is required")]
        [Display(Name = "Product Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Product Picture")]
        public string? ImgUrl { get; set; }
        public int CategoryId { get; set; }
        //public ProductCategoryModel CategoryModel { get; set; } = new ProductCategoryModel();


        public static implicit operator ProductEntity(ProductRegisterModel productRegisterModel) //viewmodel ska kunna bli en productentity
        {
            return new ProductEntity
            {
                Id = productRegisterModel.Id,
                Name = productRegisterModel.Name,
                Description = productRegisterModel.Description,
                Price = productRegisterModel.Price,
                ImgUrl = productRegisterModel.ImgUrl,
                CategoryId = productRegisterModel.CategoryId
            };
        }
    }
}
