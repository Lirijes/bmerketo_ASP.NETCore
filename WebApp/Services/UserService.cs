﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.Models.Enteties;
using WebApp.Models.Identity;
using WebApp.ViewModels;

namespace WebApp.Services;

public class UserService
{
    private readonly DataContext _context;
    private readonly UserManager<CustomIdentityUser> _userManager;
    private readonly IdentityContext _identityContext;
    private readonly SignInManager<CustomIdentityUser> _signInManager;
    private readonly SeedService _seedService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(DataContext context, UserManager<CustomIdentityUser> userManager, IdentityContext identityContext, SignInManager<CustomIdentityUser> signInManager, SeedService seedService, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _identityContext = identityContext;
        _signInManager = signInManager;
        _seedService = seedService;
        _roleManager = roleManager;
    }

    public async Task<bool> DoUserExists(Expression<Func<UserEntity, bool>> predicate)
    {
        if (await _context.Users.AnyAsync(predicate))
            return true;

        return false;
    }

    public async Task<ProfileEntity> GetUserProfileAsync(string userId)
    {
        var userProfileEntity = await _identityContext.Profiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);
        return userProfileEntity!;
    }

    public async Task<CustomIdentityUser> GetUserById(string id)
    {
        var user = await _identityContext.Users.Include(x => x.Id).FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public async Task<IEnumerable<ProfileEntity>> GetAllProfilesAsync()
    {
        var profiles = new List<ProfileEntity>();
        var items = await _identityContext.Profiles.ToListAsync();
        foreach (var item in items)
        {
            ProfileEntity profileEntity = item;
            profiles.Add(profileEntity);
        }
        return profiles;
    }

    public async Task<List<UserWithRoleModel>> GetAllUsersWithRolesAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var result = new List<UserWithRoleModel>();
        foreach (var user in users)
        {
            var userRoleModel = new UserWithRoleModel
            {
                Profile = await GetUserProfileAsync(user.Id),
                User = user,
                Roles = await GetUserRoleAsync(user.Id)
            };
            result.Add(userRoleModel);
        }
        return result;
    }

    public async Task<IList<string>> GetUserRoleAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (roles != null)
            {
                return roles;
            }
        }
        return null!;
    }

    public async Task<List<IdentityRole>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return roles;
    }

    //vet ej om dessa fungerar för att uppdatera roller?????
    public async Task UpdateUserRoleAsync(string userId, )
    {
        var userRole = await _identityContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);

        if (userRole != null)
        {
            userRole.RoleId = roleId;

            _identityContext.Update(userRole);
            await _identityContext.SaveChangesAsync();
        }
    }
    public async Task UpdateUsersRoleAsync(string userId, string roleId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var role = await _roleManager.FindByIdAsync(roleId);

        if (user != null && role != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRoleAsync(user, role.Id);
        }
    }
    public async Task UpdateUserEmployeeRoleAsync(string userId, string roleId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var role = await _roleManager.FindByIdAsync(roleId);

        if (user != null && role != null)
        {
            var allRoles = await _roleManager.Roles.ToListAsync();

            foreach (var r in allRoles)
            {
                if (await _userManager.IsInRoleAsync(user, r.Name))
                {
                    await _userManager.RemoveFromRoleAsync(user, r.Name);
                }
            }

            await _userManager.AddToRoleAsync(user, role.Name);
        }
    }







    public async Task<bool> RegisterAsync(RegisterViewModel viewModel)
    {
        try
        {
            await _seedService.SeedRoles();
            var roleName = "user";

            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
                await _roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (!await _userManager.Users.AnyAsync()) //om vi inte har några användare så blir den första användaren admin
                roleName = "admin";

            //skapar en användare
            CustomIdentityUser identityUser = viewModel;
            await _userManager.CreateAsync(identityUser, viewModel.Password);

            await _userManager.AddToRoleAsync(identityUser, roleName);

            //skapar en profil för användaren
            ProfileEntity profileEntity = viewModel;
            profileEntity.UserId = identityUser.Id;

            _identityContext.Profiles.Add(profileEntity); //konverterar om registerviewmodel til en profil
            await _identityContext.SaveChangesAsync();

            return true;
        }
        catch { return false; }
    }

    public async Task<bool> LoginAsync(LoginViewModel loginViewModel)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false);
                return result.Succeeded;
        } 
        catch { return false; }
    }

    public async Task<bool> LogoutAsync(ClaimsPrincipal user)
    {
        await _signInManager.SignOutAsync();
        return _signInManager.IsSignedIn(user);
    }
}
