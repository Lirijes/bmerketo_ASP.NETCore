using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApp.Contexts;
using WebApp.Models.Enteties;
using WebApp.ViewModels;

namespace WebApp.Services;

public class UserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> DoUserExists(Expression<Func<UserEntity, bool>> predicate)
    {
        if (await _context.Users.AnyAsync(predicate))
            return true;

        return false;
    }

    public async Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(predicate);
        return userEntity!;
    }

    public async Task<bool> RegisterAsync(RegisterViewModel registerViewModel)
    {
        try
        {
            UserEntity userEntity = registerViewModel; //kommer ta implicit operatior och göra om den till en userentity
            ProfileEntity profileEntity = registerViewModel;

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();

            _context.Profiles.Add(profileEntity);
            profileEntity.UserId = userEntity.Id;
            await _context.SaveChangesAsync();

            return true;
        } catch 
        {
            return false;
        }
    }

    public async Task<bool> LoginAsync(LoginViewModel loginViewModel)
    {
        var userEntity = await GetAsync(x => x.Email == loginViewModel.Email);
        if (userEntity != null)
            return userEntity.VerifySecurePassword(loginViewModel.Password);

        return false;
    }
}
