using FastFood.Application.Dtos.Category;
using FastFood.Application.Presenters;
using FastFood.Application.UseCases;
using FastFood.DataSource;
using FastFood.Gateway;

namespace FastFood.CoreController
{
    public class CategoryController
    {
        private readonly IDataSource _dataSource;
        private readonly CategoryGateway _gateway;
        private readonly CategoryUseCases _useCase;
        private readonly CategoryPresenter _presenter;

        public CategoryController(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _gateway = new CategoryGateway(_dataSource);
            _useCase = new CategoryUseCases();
            _presenter = new CategoryPresenter();
        }

        public async Task<IEnumerable<ResponseCategoryDto>> GetCategories()
        {
            var categories = await _gateway.GetCategories();

            return _presenter.ToResponseCategoryDtos(categories);
        }

        public async Task<ResponseCategoryDto> GetCategoryById(int id)
        {
            var category = await _gateway.GetCategoryById(id);

            return _presenter.ToResponseCategoryDto(category);
        }

        public async Task<UseCaseResult<ResponseCategoryDto>> SaveCategory(SaveCategoryDto categoryDto)
        {
            var category = _gateway.ToEntity(categoryDto);
            var useCaseResult = await _useCase.SaveCategory(category);

            if (useCaseResult.IsSuccess)
            {
                await _gateway.SaveCategoryAsync(category);
            }

            return _presenter.ToResponseCategoryDto(useCaseResult);
        }

        public async Task<UseCaseResult<ResponseCategoryDto>> UpdateCategoryById(int id, SaveCategoryDto categoryDto)
        {
            var existingCategory = _gateway.GetCategoryById(id);
            var newCategory = _gateway.ToEntityUpdate(id, categoryDto);

            var useCaseResult = await _useCase.UpdateCategoryById(existingCategory.Result, newCategory);

            if (useCaseResult.IsSuccess)
            {
                await _gateway.UpdateCategoryByIdAsync(newCategory);
            }

            return _presenter.ToResponseCategoryDto(useCaseResult);
        }

        public async Task<UseCaseResult<ResponseCategoryDto>> DeleteCategoryById(int id)
        {
            var existingCategory = _gateway.GetCategoryById(id);

            var useCaseResult = await _useCase.DeleteCategoryById(existingCategory.Result);

            if (useCaseResult.IsSuccess)
            {
                await _gateway.DeleteCategoryByIdAsync(id);
            }

            return _presenter.ToResponseCategoryDto(useCaseResult);
        }
    }

}
