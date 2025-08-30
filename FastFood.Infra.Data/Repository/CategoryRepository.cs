using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infra.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FastFoodDbContext _context;
        public CategoryRepository(FastFoodDbContext context)
        {
            _context = context;
        }
        public async Task DeleteCategoryByIdAsync(int id)
        {
            await _context.Categories
                .Where(x => x.Id.Equals(id))
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? new Category();
        }

        public async Task SaveCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryByIdAsync(Category category)
        {
            await _context.Categories
                .Where(x => x.Id.Equals(category.Id))
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Name, category.Name));
        }
    }
}
