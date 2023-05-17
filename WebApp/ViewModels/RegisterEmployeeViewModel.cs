using Microsoft.AspNetCore.Identity;
using WebApp.Models.Identity;

namespace WebApp.ViewModels
{
    public class RegisterEmployeeViewModel
    {
        public RegisterViewModel Register { get; set; }
        public CustomIdentityUser User { get; set; }
        public IdentityRole Roles { get; set; }
    }
}
