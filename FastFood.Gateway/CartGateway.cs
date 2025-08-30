using FastFood.Application.Dtos.Cart;
using FastFood.Application.Dtos.CartItem;
using FastFood.Application.Dtos.User;
using FastFood.Application.UseCases;
using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Repository;

namespace FastFood.Gateway
{
    public class CartGateway
    {
        private readonly IDataSource _dataSource;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;

        public CartGateway(IDataSource dataSource) 
        {
            _dataSource = dataSource;
            _cartRepository = new CartRepository(_dataSource.GetFastFoodContext());
            _cartItemRepository = new CartItemRepository(_dataSource.GetFastFoodContext());
            _productRepository = new ProductRepository(_dataSource.GetFastFoodContext());
        }

        public Cart CreateCartByUserId(Guid id)
        {
            return Cart.CreateCart(id);
        }

        public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            try
            {
                return await _cartRepository.GetAllCartsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<Cart> GetCartAsync(int id)
        {
            try
            {
                return await _cartRepository.GetCartByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<Cart> GetUserCartAsync(Guid id)
        {
            try
            {
                return await _cartRepository.GetCartByUserIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            try
            {
                return await _cartRepository.InsertCartAsync(cart);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<UseCaseResult> DeleteCartByIdAsync(int id)
        {
            try
            {
                await _cartRepository.DeleteCartByIdAsync(id);

                return UseCaseResult.Success();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            try
            {
                return await _cartRepository.UpdateCartAsync(cart);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }
    }
}
