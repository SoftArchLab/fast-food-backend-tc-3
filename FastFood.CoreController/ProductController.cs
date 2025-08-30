using FastFood.Application.Dtos.Product;
using FastFood.Application.Dtos.User;
using FastFood.Application.Presenters;
using FastFood.Application.UseCases;
using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.CoreController
{
    public class ProductController
    {
        private readonly IDataSource _dataSource;
        private readonly ProductGateway _gateway;
        private readonly ProductUseCases _useCase;
        private readonly ProductPresenter _presenter;

        public ProductController(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _gateway = new ProductGateway(_dataSource);
            _useCase = new ProductUseCases();
            _presenter = new ProductPresenter();
        }

        public async Task<IEnumerable<ResponseProductDto>> GetProducts()
        {
            var response = await _gateway.GetProducts();

            return _presenter.ToResponseProductDtos(response);
        }
        public async Task<ResponseProductDto> GetProductById(int id)
        {
            var response = await _gateway.GetProductById(id);

            return _presenter.ToResponseProductDto(response);
        }
        public async Task<UseCaseResult<ResponseProductDto>> AddProduct(UpdateProductDto productDto)
        {
            var product = _gateway.ToEntity(productDto);
            var useCase = await _useCase.AddProduct(product);

            if (useCase.IsSuccess)
            {
                await _gateway.InsertProductAsync(product);
            }

            return _presenter.ToResponseProductDto(useCase);
        }
        public async Task<UseCaseResult<ResponseProductDto>> EditProduct(int id, UpdateProductDto productDto)
        {
            var product = _gateway.ToEntityUpdate(id, productDto);
            var useCase = await _useCase.UpdateProduct(product);

            if (useCase.IsSuccess)
            {
                await _gateway.UpdateProductByIdAsync(product);
            }

            return _presenter.ToResponseProductDto(useCase);
        }
        public async Task<UseCaseResult> DeleteProduct(int id)
        {
            var useCase = await _useCase.DeleteProduct(id);

            if (useCase.IsSuccess)
            {
                await _gateway.DeleteProductByIdAsync(id);
            }

            return useCase;
        }
    }
}
