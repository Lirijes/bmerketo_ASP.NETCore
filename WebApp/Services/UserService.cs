using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApp.Contexts;
using WebApp.Models.Enteties;
using WebApp.Models.Identity;
using WebApp.ViewModels;

namespace WebApp.Services;

public class UserService
{
    private readonly DataContext _context;
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly IdentityContext _identityContext;
    private readonly UserManager<IdentityUser> _userManagerIdentityUser;

    public UserService(DataContext context, UserManager<CustomIdentityUser> userManager, IdentityContext identityContext, UserManager<IdentityUser> userManagerIdentityUser)
    {
        _context = context;
        _userManager = userManager;
        _identityContext = identityContext;
        _userManagerIdentityUser = userManagerIdentityUser;
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

    public async Task<bool> RegisterAsync(RegisterViewModel viewModel)
    {
        try
        {
            //skapar en användare
            IdentityUser identityUser = viewModel;
            await _userManagerIdentityUser.CreateAsync(identityUser, viewModel.Password);

            //skapar en profil för användaren
            ProfileEntity profileEntity = viewModel;
            profileEntity.UserId = identityUser.Id;

            _identityContext.Profiles.Add(profileEntity); //konverterar om registerviewmodel til en profil
            await _identityContext.SaveChangesAsync();

            return true;
        }
        catch { return false; }
    }

    //public async Task<bool> RegisterAsync(RegisterViewModel registerViewModel)
    //{
    //    try
    //    {
    //        UserEntity userEntity = registerViewModel; //kommer ta implicit operatior och göra om den till en userentity
    //        ProfileEntity profileEntity = registerViewModel;

    //        _context.Users.Add(userEntity);
    //        await _context.SaveChangesAsync();

    //        _context.Profiles.Add(profileEntity);
    //        profileEntity.UserId = userEntity.Id;
    //        await _context.SaveChangesAsync();

    //        return true;
    //    } catch 
    //    {
    //        return false;
    //    }
    //}

    public async Task<bool> LoginAsync(LoginViewModel loginViewModel)
    {
        var userEntity = await GetAsync(x => x.Email == loginViewModel.Email);
        if (userEntity != null)
            return userEntity.VerifySecurePassword(loginViewModel.Password);

        return false;
    }
}
