using WebApp.Models;

namespace WebApp.ViewModels
{
    public class UsersIndexViewModel
    {
        public string Title { get; set; } = "Users";
        public List<UserWithRoleModel> UsersWithRoles { get; set; }
        public IList<string> Roles { get; set; }
    }
}
