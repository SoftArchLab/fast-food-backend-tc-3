using FastFood.Application.Dtos.Category;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;

namespace FastFood.Application.Interfaces
{
    public interface ICategoryUseCases
    {
        //public Task<UseCaseResult<IEnumerable<Category>>> GetCategories();
        //public Task<UseCaseResult<Category>> GetCategoryById(int categoryId);
        public Task<UseCaseResult<Category>> SaveCategory(Category category);
        public Task<UseCaseResult<Category>> UpdateCategoryById(Category? existingCategory, Category newCategory);
        public Task<UseCaseResult<Category>> DeleteCategoryById(Category? category);
    }
}
