using FastFood.Application.Dtos.Category;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;

namespace FastFood.Application.Presenters
{
    public class CategoryPresenter
    {
        public ResponseCategoryDto ToResponseCategoryDto(Category category)
        {
            if (category == null)
            {
                return null;
            }
            return new ResponseCategoryDto
            {
                CategoryId = category.Id,
                CategoryName = category.Name
            };
        }
        public UseCaseResult<ResponseCategoryDto> ToResponseCategoryDto(UseCaseResult<Category> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<ResponseCategoryDto>.Failure(useCaseResult?.Message);
            }
            var responseCategoryDto = ToResponseCategoryDto(useCaseResult.Data);
            return UseCaseResult<ResponseCategoryDto>.Success(responseCategoryDto, useCaseResult.Message);
        }

        public IEnumerable<ResponseCategoryDto> ToResponseCategoryDtos(IEnumerable<Category> categories)
        {
            if (categories == null || !categories.Any())
            {
                return Enumerable.Empty<ResponseCategoryDto>();
            }
            return categories.Select(ToResponseCategoryDto);
        }
        public UseCaseResult<IEnumerable<ResponseCategoryDto>> ToResponseCategoryDtos(UseCaseResult<IEnumerable<Category>> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<IEnumerable<ResponseCategoryDto>>.Failure(useCaseResult?.Message);
            }
            var responseCategoryDtos = ToResponseCategoryDtos(useCaseResult.Data);
            return UseCaseResult<IEnumerable<ResponseCategoryDto>>.Success(responseCategoryDtos, useCaseResult.Message);
        }
    }
}
