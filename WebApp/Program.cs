using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //denna möjliggör dependency injections för våra controllers 
builder.Services.AddScoped<ShowcaseService>(); // hanterar automatiskt skapandet av new ShowcaseService() den gör instansiering på det sätt som scoped hanterar det på
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Sql"))); //här vill vi använda oss utav datacontext med dependency injections och använda oss av sqlserver. I sqlserver använder vi en connection string

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
