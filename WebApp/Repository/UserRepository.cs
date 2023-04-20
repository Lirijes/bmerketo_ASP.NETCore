using WebApp.Contexts;
using WebApp.Models.Enteties;

namespace WebApp.Repository
{
    public class UserRepository : Repository<ProfileEntity>
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
