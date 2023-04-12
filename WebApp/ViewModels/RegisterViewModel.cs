using System.ComponentModel.DataAnnotations;
using WebApp.Models.Enteties;
using WebApp.Models.Identity;

namespace WebApp.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Du måste ange ett förnamn")]
    [Display(Name = "Firstname")]
    [RegularExpression(@"^[a-ÖA-Ö]+(?:[ é-ë'-][a-öA-Ö]+)*$", ErrorMessage = "Firstname is required")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Du måste ange ett efternamn")]
    [Display(Name = "Lastname")]
    [RegularExpression(@"^[a-ÖA-Ö]+(?:[ é-ë'-][a-öA-Ö]+)*$", ErrorMessage = "Lastname is required")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Phonenumber")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Email-address is required")]
    [Display(Name = "Email-address")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "You have to enter a valid email-address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "You have to enter a valid password (at least 1 capital letter, 1 lowcase letter, 1 number and 1 special character")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Please confirm the password")]
    [Compare(nameof(Password), ErrorMessage = "Password does not match")]
    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "Address")]
    public string? StreetName { get; set; }

    [Display(Name = "Postalcode")]
    public string? PostalCode { get; set; }

    [Display(Name = "City")]
    public string? City { get; set; }

    [Display(Name = "Company")]
    public string? Company { get; set; }

    [Display(Name = "Picture")]
    public string? Picture { get; set; }

    //konverterar från registreringsmodellen till en userentity
    public static implicit operator UserEntity(RegisterViewModel registerViewModel)
    {
        var userEntity = new UserEntity
        {
            Email = registerViewModel.Email
        };
        userEntity.GenerateSecurePassword(registerViewModel.Password);
        return userEntity;
    }

    public static implicit operator ProfileEntity(RegisterViewModel registerViewModel)
    {
        var profileEntity = new ProfileEntity
        {
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            PhoneNumber = registerViewModel.PhoneNumber,
            StreetName = registerViewModel.StreetName,
            PostalCode = registerViewModel.PostalCode,
            City = registerViewModel.City,
            Company = registerViewModel.Company
        };

        return profileEntity;
    }

    public static implicit operator CustomIdentityUser(RegisterViewModel viewModel) //registerviewmodel ska kunna bli en customidentityuser
    {
        return new CustomIdentityUser
        {
            UserName = viewModel.Email,
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber
        };
    }
}
