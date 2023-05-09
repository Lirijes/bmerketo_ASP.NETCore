using System.ComponentModel.DataAnnotations;
using WebApp.Models.Enteties;

namespace WebApp.ViewModels
{
    public class ProductRegistrationViewModel
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
        [DataType(DataType.Upload)]
        public IFormFile? ImgUrl { get; set; }
        public int CategoryId { get; set; }


        public static implicit operator ProductEntity(ProductRegistrationViewModel productRegisterModel) //viewmodel ska kunna bli en productentity
        {
            var entity = new ProductEntity
            {
                Id = productRegisterModel.Id,
                Name = productRegisterModel.Name,
                Description = productRegisterModel.Description,
                Price = productRegisterModel.Price,
                //ImgUrl = productRegisterModel.ImgUrl,
                CategoryId = productRegisterModel.CategoryId
            };

            if (productRegisterModel.ImgUrl != null)
                entity.ImgUrl = $"{productRegisterModel.Id}_{productRegisterModel.ImgUrl?.FileName}"; //bildens namn kmr va artnr_namnet på bilden

            return entity;
        }
    }
}
