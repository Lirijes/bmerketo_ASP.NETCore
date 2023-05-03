using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Models.Enteties;
using WebApp.ViewModels;

namespace WebApp.Services;

public class ContactsService
{
    private readonly DataContext _context;

    public ContactsService(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(ContactUsViewModel contactUsViewModel)
    {
        try
        {
            ContactsEntity contactsEntity = contactUsViewModel;

            _context.Contacts.Add(contactsEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch { return false; }
    }

    public async Task<IEnumerable<ContactsEntity>> GetAllAsync()
    {
        var comments = new List<ContactsEntity>();
        var items = await _context.Contacts.ToListAsync();
        foreach (var item in items)
        {
            ContactsEntity contactsEntity = item;
            comments.Add(contactsEntity);
        }
        return comments;
    }
}
