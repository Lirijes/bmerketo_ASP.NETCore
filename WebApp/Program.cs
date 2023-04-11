using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Models.Identity;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //denna möjliggör dependency injections för våra controllers 
builder.Services.AddScoped<ShowcaseService>(); // hanterar automatiskt skapandet av new ShowcaseService() den gör instansiering på det sätt som scoped hanterar det på
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddDbContext<IdentityContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySql")));
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Sql"))); //här vill vi använda oss utav datacontext med dependency injections och använda oss av sqlserver. I sqlserver använder vi en connection string

builder.Services.AddIdentity<CustomIdentityUser, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedAccount = false;
    x.User.RequireUniqueEmail = true;
    x.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<IdentityContext>(); //för att den ska veta vilken dbcontext den ska använda för att hantera detta

var app = builder.Build();
app.UseHsts(); //certifikatsdelar 
app.UseHttpsRedirection(); //omdirigerar trafiken från http till https
app.UseStaticFiles(); //använda wwwroot delen (statiska filer)
app.UseRouting(); //hanterar routing i controllers
app.UseAuthorization(); //vi kan begränsa tillgänglighten på sidor
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
