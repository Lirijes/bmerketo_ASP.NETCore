using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Models.Enteties;
using WebApp.Repository;
using WebApp.ViewModels;

namespace WebApp.Services;

public class ContactsService
{
    private readonly DataContext _context;
    private readonly ContactRepository _contactRepository;

    public ContactsService(DataContext context, ContactRepository contactRepository)
    {
        _context = context;
        _contactRepository = contactRepository;
    }

    //denna del är hämtad från repositoryn
    public async Task<ContactsEntity> GetOrCreateAsync(ContactsEntity contactsEntity)
    {
        var entity = await _contactRepository.GetAsync(x =>
            x.Id == contactsEntity.Id &&
            x.Name == contactsEntity.Name &&
            x.Email == contactsEntity.Email &&
            x.Phone == contactsEntity.Phone &&
            x.Comment == contactsEntity.Comment
        );
        entity ??= await _contactRepository.AddAsync(contactsEntity);
        return entity;
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
