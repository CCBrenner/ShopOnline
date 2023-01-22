using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Respositories.Contracts;
using ShopOnline.Models.Dtos;
using System.Reflection.Metadata.Ecma335;

namespace ShopOnline.Api.Respositories
{
    public class ProductRepository : IProductRepository
    {

        public ProductRepository(ShopOnlineDbContext shopOnlineDbContext) =>
            this.shopOnlineDbContext = shopOnlineDbContext;
        private readonly ShopOnlineDbContext shopOnlineDbContext;

        public async Task<IEnumerable<ProductCategory>> GetCategories() => 
            await shopOnlineDbContext.ProductCategories.ToListAsync();

        public async Task<ProductCategory> GetCategory(int id) =>
            await shopOnlineDbContext.ProductCategories.FindAsync(id);

        public async Task<Product> GetItem(int id) =>
            await shopOnlineDbContext.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> GetItems() =>
            await shopOnlineDbContext.Products.ToListAsync();

        public async Task<IEnumerable<Product>> GetItemsByCategory(int categoryId)
        {
            return await this.shopOnlineDbContext.Products
                .Include(p => p.ProductCategory)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
