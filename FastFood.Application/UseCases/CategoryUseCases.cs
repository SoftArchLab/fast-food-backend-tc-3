using FastFood.Domain.Entities;
using FastFood.Application.Interfaces;
using FastFood.Domain.Interfaces;
using FastFood.Application.Dtos.Category;

namespace FastFood.Application.UseCases
{
    public class CategoryUseCases : ICategoryUseCases
    {
        public ICategoryRepository _categoryRepository { get; set; }

        //public CategoryUseCases(ICategoryRepository categoryRepository) 
        //{ 
        //    _categoryRepository = categoryRepository;
        //}

        public CategoryUseCases()
        {

        }

        //public async Task<UseCaseResult<IEnumerable<Category>>> GetCategories()
        //{
        //    try
        //    {
        //        var categories = await _categoryRepository.GetCategoriesAsync();

        //        return UseCaseResult<IEnumerable<Category>>.Success(categories);
        //    }
        //    //TODO: Implementar DomainException
        //    catch (Exception ex)
        //    {
        //        return UseCaseResult<IEnumerable<Category>>.Failure("Ocorreu um erro inesperado: " + ex.Message);
        //    }

        //}

        //public async Task<UseCaseResult<Category>> GetCategoryById(int categoryId)
        //{
        //    try
        //    {
        //        var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);

        //        return UseCaseResult<Category>.Success(category);
        //    }
        //    //TODO: Implementar DomainException
        //    catch (Exception ex)
        //    {
        //        return UseCaseResult<Category>.Failure("Ocorreu um erro inesperado: " + ex.Message);
        //    }
        //}

        public async Task<UseCaseResult<Category>> SaveCategory(Category category)
        {
            if (category == null)
                return UseCaseResult<Category>.Failure("Categoria não pode ser nula");
            else if (!category.ValidadeSaveCategory(category.Name))
                return UseCaseResult<Category>.Failure("Nome da categoria inválida");

            return UseCaseResult<Category>.Success(category);
            //try
            //{
            //    var category = new Category();
            //    category.AddCategory(categoryDto.Name);

            //    await _categoryRepository.SaveCategoryAsync(category);

            //    return UseCaseResult<Category>.Success(category);
            //}
            ////TODO: Implementar DomainException
            //catch (Exception ex)
            //{
            //    return UseCaseResult<Category>.Failure("Ocorreu um erro inesperado: " + ex.Message);
            //}
        }

        //public async Task<UseCaseResult> UpdateCategoryById(int categoryId, SaveCategoryDto categoryDto)
        public async Task<UseCaseResult<Category>> UpdateCategoryById(Category? existingCategory, Category newCategory)
        {
            if (existingCategory == null)
                return UseCaseResult<Category>.Failure("Categoria não pode ser nula");

            try
            {
                existingCategory.UpdateCategory(existingCategory.Id, newCategory.Name);

                return UseCaseResult<Category>.Success(newCategory);

            }
            catch (ArgumentException ex)
            {
                return UseCaseResult<Category>.Failure(ex.Message);
            }

            //try
            //{
            //    var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            //    if(category == null)
            //        return UseCaseResult<Category>.Failure("Categoria não encontrada");

            //    category.UpdateCategory(categoryId, categoryDto.Name);

            //    await _categoryRepository.UpdateCategoryByIdAsync(category);

            //    return UseCaseResult<Category>.Success(category);
            //}
            ////TODO: Implementar DomainException
            //catch (Exception ex)
            //{
            //    return UseCaseResult<Category>.Failure("Ocorreu um erro inesperado: " + ex.Message);
            //}
        }

        public async Task<UseCaseResult<Category>> DeleteCategoryById(Category? category)
        {
            if (category == null)
                return UseCaseResult<Category>.Failure("Categoria não pode ser nula.");
            else if (category.Id <= 0)
                return UseCaseResult<Category>.Failure("Id da categoria inválido.");
            else if (category.Products.Any())
                return UseCaseResult<Category>.Failure("Não é possível remover uma categoria que possuí produtos vinculados.");

            return UseCaseResult<Category>.Success(category);

            //try
            //{
            //    await _categoryRepository.DeleteCategoryByIdAsync(categoryId);
            //    return UseCaseResult.Success();
            //}
            ////TODO: Implementar DomainException
            //catch (Exception ex)
            //{
            //    return UseCaseResult.Failure("Ocorreu um erro inesperado: " + ex.Message);
            //}
        }
    }
}
