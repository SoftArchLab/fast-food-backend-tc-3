using FastFood.Application.Dtos.Product;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Presenters
{
    public class ProductPresenter
    {
        public ResponseProductDto ToResponseProductDto(Product product)
        {
            if (product == null)
            {
                return null;
            }
            return new ResponseProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
            };
        }
        public UseCaseResult<ResponseProductDto> ToResponseProductDto(UseCaseResult<Product> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<ResponseProductDto>.Failure(useCaseResult?.Message);
            }
            var responseProductDto = ToResponseProductDto(useCaseResult.Data);
            return UseCaseResult<ResponseProductDto>.Success(responseProductDto, useCaseResult.Message);
        }

        public IEnumerable<ResponseProductDto> ToResponseProductDtos(IEnumerable<Product> products)
        {
            if (products == null || !products.Any())
            {
                return Enumerable.Empty<ResponseProductDto>();
            }
            return products.Select(ToResponseProductDto);
        }
        public UseCaseResult<IEnumerable<ResponseProductDto>> ToResponseProductDtos(UseCaseResult<IEnumerable<Product>> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<IEnumerable<ResponseProductDto>>.Failure(useCaseResult?.Message);
            }
            var reponseProductDtos = ToResponseProductDtos(useCaseResult.Data);
            return UseCaseResult<IEnumerable<ResponseProductDto>>.Success(reponseProductDtos, useCaseResult.Message);
        }
    }
}
