using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Linq.Expressions;
using WebApp.Contexts;
using WebApp.Models;
using WebApp.Models.Enteties;
using WebApp.Repository;
using WebApp.ViewModels;

namespace WebApp.Services;

public class ProductService
{
    private readonly DataContext _context;
    private readonly ProductRepo _productRepo;

    public ProductService(DataContext context, ProductRepo productRepo)
    {
        _context = context;
        _productRepo = productRepo;
    }

    public async Task<bool> CreateAsync(ProductRegistrationViewModel productRegistrationViewModel)
    {
        try
        {
            ProductEntity productEntity = productRegistrationViewModel.Form; //den skapas på vår viewmodel

            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch { return false; }
    }

    public async Task<ProductEntity> GetAsync(int id)
    {
        var entity = await _context.Set<ProductEntity>().FirstOrDefaultAsync(x => x.Id == id);
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

    //public async Task<ProductEntity> GetTagAsync(ProductCategoryEntity productCategory)
    //{
    //    //var bestCollectionProducts = _context.Products.Where(p => p.CategoryId == productCategory.Id).ToList();

    //    var tag = await _context.Products.Where(x => x.Category.All(x => x.Id == productCategory.Id)).Select(x => x.Id).ToListAsync();
    //    //var tag = await _context.ProductCategories.Where(x => x.Products.All(x => x.Category.Select(x => x.Id)).Contains());
    //    if (tag == 1)
    //    {

    //    }
    //}

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
        return await _productRepo.GetAllAsync();
    }
}
