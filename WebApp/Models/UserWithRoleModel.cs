using WebApp.Models.Enteties;
using WebApp.Models.Identity;

namespace WebApp.Models
{
    public class UserWithRoleModel
    {
        public ProfileEntity Profile { get; set; }
        public CustomIdentityUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
