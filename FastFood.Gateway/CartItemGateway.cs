using FastFood.Application.Dtos.CartItem;
using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Gateway
{
    public class CartItemGateway
    {
        private readonly IDataSource _dataSource;
        private readonly ICartItemRepository _cartItemRepository;
        
        public CartItemGateway(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _cartItemRepository = new CartItemRepository(_dataSource.GetFastFoodContext());
        }

        public CartItem ToEntity(CartItemDto cartItemDto)
        {
            return CartItem.Create(cartItemDto.Quantity, cartItemDto.ProductId, cartItemDto.CartId);
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            try
            {
               await _cartItemRepository.InsertCartItemAsync(cartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            try
            {
                await _cartItemRepository.UpdateCartItemAsync(cartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task RemoveCartItemAsync(int id)
        {
            try
            {
                await _cartItemRepository.DeleteCartItemByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }
    }
}
