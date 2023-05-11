using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Models.Enteties;

namespace WebApp.Contexts;

public class DataContext : DbContext //komminukationen mellan databasen och systemet
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ProfileEntity> Profiles { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ContactsEntity> Contacts { get; set; }
    public DbSet<ImagesEntity> Images { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
}
