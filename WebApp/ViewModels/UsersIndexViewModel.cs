using WebApp.Models;
using WebApp.Models.Identity;

namespace WebApp.ViewModels
{
    public class UsersIndexViewModel
    {
        public string Title { get; set; } = "Users";
        public List<UserWithRoleModel> UsersWithRoles { get; set; }
        public IList<string> Roles { get; set; }
        public CustomIdentityUser User { get; set; }
    }
}
