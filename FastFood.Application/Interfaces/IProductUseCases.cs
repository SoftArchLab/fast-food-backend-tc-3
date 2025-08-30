using FastFood.Application.Dtos.Product;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;

namespace FastFood.Application.Interfaces
{
    public interface IProductUseCases
    {
        Task<UseCaseResult<Product>> AddProduct(Product product);
        Task<UseCaseResult<Product>> UpdateProduct(Product product);
        Task<UseCaseResult> DeleteProduct(int id);
        Task<UseCaseResult> GetProductsByCategoryId(int categoryId);
    }
}
