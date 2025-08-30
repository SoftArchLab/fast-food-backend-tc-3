using FastFood.Application.Dtos.Product;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FastFood.Domain.Exceptions;
using FastFood.Domain.Interfaces;
using MercadoPago.Resource.User;

namespace FastFood.Application.UseCases
{
    public class ProductUseCases : IProductUseCases
    {
        protected readonly IProductRepository _productRepository;

        public ProductUseCases()
        {
        }

        public async Task<UseCaseResult<Product>> AddProduct(Product product)
        {
            try
            {
                if (product == null)
                    return UseCaseResult<Product>.Failure("Produto não pode ser nulo");

                if (!product.ValidateName(product.Name))
                    return UseCaseResult<Product>.Failure("Nome inválido");

                if (!product.ValidateDescription(product.Description))
                    return UseCaseResult<Product>.Failure("Descrição inválida");

                if (!product.ValidatePrice(product.Price))
                    return UseCaseResult<Product>.Failure("Preço inválido");

                if (!product.ValidateStock(product.StockQuantity))
                    return UseCaseResult<Product>.Failure("Quantidade do estoque inválido");

                return UseCaseResult<Product>.Success(product);
            }
            catch (DomainException ex)
            {
                return UseCaseResult<Product>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult<Product>.Failure("Ocorreu um erro inesperado: " + ex.Message);
            }

        }
        public async Task<UseCaseResult> DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                    return UseCaseResult.Failure("Id inválido");

                return UseCaseResult.Success();
            }
            catch (DomainException ex)
            {
                return UseCaseResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult.Failure("Ocorreu um erro inesperado: " + ex.Message);
            }
        }
        public async Task<UseCaseResult<Product>> UpdateProduct(Product product)
        {
            try
            {
                if (product == null)
                    return UseCaseResult<Product>.Failure("Produto não pode ser nulo");

                if (!product.ValidateName(product.Name))
                    return UseCaseResult<Product>.Failure("Nome inválido");

                if (!product.ValidateDescription(product.Description))
                    return UseCaseResult<Product>.Failure("Descrição inválida");

                if (!product.ValidatePrice(product.Price))
                    return UseCaseResult<Product>.Failure("Preço inválido");

                if (!product.ValidateStock(product.StockQuantity))
                    return UseCaseResult<Product>.Failure("Quantidade do estoque inválido");

                return UseCaseResult<Product>.Success(product);
            }
            catch (DomainException ex)
            {
                return UseCaseResult<Product>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult<Product>.Failure("Ocorreu um erro inesperado: " + ex.Message);
            }
        }
        public async Task<UseCaseResult> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                if (categoryId <= 0)
                    return UseCaseResult.Failure("Id da categoria inválido");

                return UseCaseResult.Success();
            }
            catch (DomainException ex)
            {
                throw new DomainException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado: " + ex.Message);
            }
        }
    }
}
