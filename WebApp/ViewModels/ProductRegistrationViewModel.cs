using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;
using WebApp.Models.Enteties;

namespace WebApp.ViewModels;

public class ProductRegistrationViewModel
{
    public ProductEntity ProductEntity { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = null!;

    [Display(Name = "Product Description")]
    public string? Description { get; set; } = null!;

    [Required(ErrorMessage = "Product pris is required")]
    [Display(Name = "Product Price")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Display(Name = "Product Picture")]
    public IFormFile? Picture { get; set; }

    //public string? ImageLocation { get; set; }


    public static implicit operator ProductEntity(ProductRegistrationViewModel productRegistrationViewModel) //viewmodel ska kunna bli en productentity
    {
        using(var path = File.Create(Path.Combine("uploadedimg/products", productRegistrationViewModel.Picture.FileName)))
        
        return new ProductEntity
        {
            Name = productRegistrationViewModel.Name,
            Description = productRegistrationViewModel.Description,
            Price = productRegistrationViewModel.Price,
            //Images = productRegistrationViewModel.Picture.CopyTo(path)
            //ImageLocation = productRegistrationViewModel.ImageLocation
        };
    }
}
