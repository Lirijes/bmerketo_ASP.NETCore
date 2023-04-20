using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Enteties;
using WebApp.Models.Identity;

namespace WebApp.Contexts;

public class IdentityContext : IdentityDbContext<CustomIdentityUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    public DbSet<ProfileEntity> Profiles { get; set; }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);

    //    builder.Entity<IdentityRole>().HasData(
    //        new IdentityRole
    //        {
    //            Name = "system admin",
    //            NormalizedName = "SYSTEM ADMIN"
    //        }
    //    );
    //}
}
