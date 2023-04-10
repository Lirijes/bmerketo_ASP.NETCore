using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email-address is required")]
    [Display(Name = "E-Postadress")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [Display(Name = "Lösenord")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
