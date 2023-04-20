using WebApp.Models.Identity;

namespace WebApp.ViewModels
{
    public class UsersIndexViewModel
    {
        public string Title { get; set; } = "Users";
        public CustomIdentityUser All { get; set; } = null!;
    }
}
