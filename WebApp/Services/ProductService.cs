using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Models.Enteties;
using WebApp.ViewModels;

namespace WebApp.Services;

public class ProductService
{
    private readonly DataContext _context;

    public ProductService(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(ProductRegistrationViewModel productRegistrationViewModel)
    {
        try
        {
            ProductEntity productEntity = productRegistrationViewModel; //den skapas på vår viewmodel

            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch { return false; }
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        var products = new List<ProductEntity>();
        var items = await _context.Products.ToListAsync();
        foreach (var item in items)
        {
            ProductEntity productEntity = item;
            products.Add(productEntity);
        }

        return products;
    }
}
