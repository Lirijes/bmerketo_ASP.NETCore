using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.Models.Identity;

namespace WebApp.ViewModels
{
    public class UsersIndexViewModel
    {
        public string Title { get; set; } = "Users";
        public List<UserWithRoleModel> UsersWithRoles { get; set; }
        public UserRoleModel UserRoleModel { get; set; }
        public List<CustomIdentityUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}
