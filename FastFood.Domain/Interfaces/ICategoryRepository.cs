using FastFood.Domain.Entities;

namespace FastFood.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public Task SaveCategoryAsync(Category category);
        public Task<IEnumerable<Category>> GetCategoriesAsync();
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task UpdateCategoryByIdAsync(Category category);
        public Task DeleteCategoryByIdAsync(int id);
    }
}
