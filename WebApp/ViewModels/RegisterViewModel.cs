using System.ComponentModel.DataAnnotations;
using WebApp.Models.Enteties;

namespace WebApp.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Du måste ange ett förnamn")]
    [Display(Name = "Förnamn")]
    [RegularExpression(@"^[a-ÖA-Ö]+(?:[ é-ë'-][a-öA-Ö]+)*$", ErrorMessage = "Du måste ange ett giltigt förnamn")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Du måste ange ett efternamn")]
    [Display(Name = "Efternamn")]
    [RegularExpression(@"^[a-ÖA-Ö]+(?:[ é-ë'-][a-öA-Ö]+)*$", ErrorMessage = "Du måste ange ett giltigt efternamn")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Telefonnummer")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "Du måste ange en e-postadress")]
    [Display(Name = "E-Postadress")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Du måste ange en giltigt e-postadress")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Du måste ange ett lösenord")]
    [Display(Name = "Lösenord")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Du måste ange ett giltigt lösenord")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Du måste bekräfta lösenordet")]
    [Compare(nameof(Password), ErrorMessage = "Lösenorden matchar ej")]
    [Display(Name = "Bekräfta lösenord")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "Adress")]
    public string? StreetName { get; set; }

    [Display(Name = "Postnummer")]
    public string? PostalCode { get; set; }

    [Display(Name = "Stad")]
    public string? City { get; set; }

    [Display(Name = "Företag")]
    public string? Company { get; set; }

    [Display(Name = "Bild")]
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
}
