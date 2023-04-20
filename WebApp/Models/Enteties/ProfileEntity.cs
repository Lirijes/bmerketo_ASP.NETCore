using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.Models.Identity;

namespace WebApp.Models.Enteties;

public class ProfileEntity
{
    [Key, ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? StreetName { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? Company { get; set; }
    
    public CustomIdentityUser User { get; set; } = null!;
}