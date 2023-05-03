using Microsoft.AspNetCore.Identity;

namespace WebApp.Services;

public class SeedService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedRoles()
    {
        if (!await _roleManager.RoleExistsAsync("admin"))
            await _roleManager.CreateAsync(new IdentityRole("admin"));

        if (!await _roleManager.RoleExistsAsync("user"))
            await _roleManager.CreateAsync(new IdentityRole("user"));
    }


    //idk......
    //public async Task<IdentityRole?> GetRoleAsync(string userId)
    //{
    //    var roleUser = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == userId);
    //    return roleUser;
    //}
}
