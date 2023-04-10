using System.ComponentModel.DataAnnotations;
using WebApp.Models.Enteties;

namespace WebApp.ViewModels;

public class ProductRegistrationViewModel
{
    [Required(ErrorMessage = "Product name is required")]
    [Display(Name = "Produktnamn")]
    public string Name { get; set; } = null!;

    [Display(Name = "Produktbeskrivning")]
    public string? Description { get; set; } = null!;

    [Required(ErrorMessage = "Product pris is required")]
    [Display(Name = "Produktpris")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; } 

    public static implicit operator ProductEntity(ProductRegistrationViewModel productRegistrationViewModel)
    {
        return new ProductEntity
        {
            Name = productRegistrationViewModel.Name,
            Description = productRegistrationViewModel.Description,
            Price = productRegistrationViewModel.Price
        };
    }
}
