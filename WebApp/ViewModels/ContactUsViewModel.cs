using System.ComponentModel.DataAnnotations;
using WebApp.Models.Enteties;

namespace WebApp.ViewModels;

public class ContactUsViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    [RegularExpression(@"^[a-ÖA-Ö]+(?:[ é-ë'-][a-öA-Ö]+)*$", ErrorMessage = "Firstname is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email-address is required")]
    [Display(Name = "Email-address")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "You have to enter a valid email-address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phonenumber is required")]
    [Display(Name = "Phonenumber")]
    public string Phone { get; set; } = null!;

    public string? Company { get; set; }

    [Required(ErrorMessage = "Comment is required")]
    [Display(Name = "Comment")]
    [RegularExpression(@"^[a-ÖA-Ö]+(?:[ é-ë'-][a-öA-Ö]+)*$", ErrorMessage = "Comment is required")]
    public string Comment { get; set; } = null!;


    public static implicit operator ContactsEntity(ContactUsViewModel model)
    {
        var contactEntity = new ContactsEntity
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            Company = model.Company,
            Comment = model.Comment
        };
        return contactEntity;
    }
}
