using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.Models.Enteties;

namespace WebApp.ViewModels
{
    public class UsersIndexViewModel
    {
        public string Title { get; set; } = "Users";
        public List<UserWithRoleModel> UsersWithRoles { get; set; }
    }
}
