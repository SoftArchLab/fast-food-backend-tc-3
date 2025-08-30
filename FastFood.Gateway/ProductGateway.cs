using FastFood.Application.Dtos.Product;
using FastFood.Application.Dtos.User;
using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Gateway
{
    public class ProductGateway
    {
        private readonly IDataSource _dataSource;
        private readonly IProductRepository _productRepository;

        public ProductGateway(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _productRepository = new ProductRepository(_dataSource.GetFastFoodContext());
        }
        public Product ToEntity(UpdateProductDto productDto)
        {
            return Product.Create(productDto.Name,
                                  productDto.Description,
                                  productDto.Price,
                                  productDto.StockQuantity,
                                  productDto.CategoryId);
        }
        public Product ToEntityUpdate(int id, UpdateProductDto productDto)
        {
            return Product.Update(id,
                                  productDto.Name,
                                  productDto.Description,
                                  productDto.Price,
                                  productDto.StockQuantity,
                                  productDto.CategoryId);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                return await _productRepository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                return await _productRepository.GetProductByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user.", ex);
            }
        }


        public async Task InsertProductAsync(Product user)
        {
            try
            {
                await _productRepository.AddProductAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user.", ex);
            }
        }

        public async Task UpdateProductByIdAsync(Product user)
        {
            try
            {
                await _productRepository.UpdateProductAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user.", ex);
            }
        }

        public async Task DeleteProductByIdAsync(int id)
        {
            try
            {
                await _productRepository.DeleteProductAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user.", ex);
            }
        }
    }
}
