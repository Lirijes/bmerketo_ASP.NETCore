using Microsoft.EntityFrameworkCore;
using WebApp.Contexts;
using WebApp.Models.Enteties;
using WebApp.Repository;
using WebApp.ViewModels;

namespace WebApp.Services;

public class ProductService
{
    private readonly DataContext _context;
    private readonly ProductRepo _productRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductService(DataContext context, ProductRepo productRepo, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _productRepo = productRepo;
        _webHostEnvironment = webHostEnvironment;
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

    public async Task<ProductEntity> CreateProductAsync(ProductEntity entity)
    {
        var _entity = await _productRepo.GetAsync(x => x.Id == entity.Id);
        if (_entity == null)
        {
            _entity = await _productRepo.AddAsync(entity);
            if(_entity != null)
                return _entity;
        }
        return null!;
    }

    public async Task<bool> UploadImageAsync(ProductEntity product, IFormFile img)
    {
        try
        {
            string imgPath = $"{_webHostEnvironment.WebRootPath}/images/products/{product.ImgUrl}";
            await img.CopyToAsync(new FileStream(imgPath, FileMode.Create));
            return true;
        }
        catch { return false; }
    }

    public async Task<ProductEntity> GetByIdAsync(int id)
    {
        var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
            return new ProductEntity
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                ImgUrl = entity.ImgUrl
            };
        return null!;
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

    public async Task<IEnumerable<ProductEntity>> GetAllProductsAsync()
    {
        var items = await _productRepo.GetAllAsync();
        var list = new List<ProductEntity>();
        foreach (var item in items)
            list.Add(item);
        return list;
    }
}
