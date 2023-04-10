using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //denna m�jligg�r dependency injections f�r v�ra controllers 
builder.Services.AddScoped<ShowcaseService>(); // hanterar automatiskt skapandet av new ShowcaseService() den g�r instansiering p� det s�tt som scoped hanterar det p�
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Sql"))); //h�r vill vi anv�nda oss utav datacontext med dependency injections och anv�nda oss av sqlserver. I sqlserver anv�nder vi en connection string

var app = builder.Build();
app.UseHsts(); //certifikatsdelar 
app.UseHttpsRedirection(); //omdirigerar trafiken fr�n http till https
app.UseStaticFiles(); //anv�nda wwwroot delen (statiska filer)
app.UseRouting(); //hanterar routing i controllers
app.UseAuthorization(); //vi kan begr�nsa tillg�nglighten p� sidor
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
