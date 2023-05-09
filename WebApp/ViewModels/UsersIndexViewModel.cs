using WebApp.Models.Enteties;

namespace WebApp.ViewModels
{
    public class UsersIndexViewModel
    {
        public string Title { get; set; } = "Users";
        public IEnumerable<ProfileEntity> Profiles { get; set; } = new List<ProfileEntity>();
    }
}
