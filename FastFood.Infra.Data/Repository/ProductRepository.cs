using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infra.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly FastFoodDbContext _context;

        public ProductRepository(FastFoodDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            await _context.Products
                .Where(x => x.Id.Equals(id))
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? new Product();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(x => x.CategoryId.Equals(categoryId))
                .ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _context.Products
                .Where(x => x.Id.Equals(product.Id))
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Name, product.Name)
                    .SetProperty(p => p.Description, product.Description)
                    .SetProperty(p => p.Price, product.Price)
                    .SetProperty(p => p.StockQuantity, product.StockQuantity)
                    .SetProperty(p => p.IsActive, product.IsActive)
                    .SetProperty(p => p.CategoryId, product.CategoryId));
        }
    }
}
