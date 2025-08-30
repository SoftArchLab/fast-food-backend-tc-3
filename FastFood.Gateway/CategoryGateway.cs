using FastFood.Application.Dtos.Category;
using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Repository;

namespace FastFood.Gateway
{
    public class CategoryGateway
    {
        private readonly IDataSource _dataSource;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryGateway(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _categoryRepository = new CategoryRepository(_dataSource.GetFastFoodContext());
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetCategoriesAsync();
                return categories;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao buscar a categorias.", ex);
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(id);
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao buscar a categoria.", ex);
            }
        }

        public async Task SaveCategoryAsync(Category category)
        {
            try
            {
                await _categoryRepository.SaveCategoryAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao salvar a categoria.", ex);
            }
        }

        public async Task UpdateCategoryByIdAsync(Category category)
        {
            try
            {
                await _categoryRepository.UpdateCategoryByIdAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao atualizar a categoria.", ex);
            }
        }

        public async Task DeleteCategoryByIdAsync(int id)
        {
            try
            {
                await _categoryRepository.DeleteCategoryByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao excluir a categoria.", ex);
            }
        }

        public Category ToEntity(SaveCategoryDto categoryDto)
        {
            return new Category(
                    name: categoryDto.Name
                );
        }

        public Category ToEntityUpdate(int id, SaveCategoryDto categoryDto)
        {
            return new Category(
                    id: id,
                    name: categoryDto.Name
                );
        }
    }
}
