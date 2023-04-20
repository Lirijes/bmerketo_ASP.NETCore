using WebApp.Contexts;
using WebApp.Models.Enteties;

namespace WebApp.Repository
{
    public class ContactRepository : Repository<ContactsEntity> //här används alla delar från repositoryn
    {
        public ContactRepository(DataContext context) : base(context)
        {
        }
    }
}
