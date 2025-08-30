using FastFood.Application.Dtos.CartItem;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FastFood.Domain.Exceptions;
using FastFood.Domain.Interfaces;

namespace FastFood.Application.UseCases
{
    public class CartItemUseCases : ICartItemUseCases
    {
        public CartItemUseCases() {}

        public async Task<UseCaseResult<CartItem>> ValidateCartItem(CartItem cartItem)
        {
            try
            {
                if (cartItem == null)
                    throw new DomainException("Item do carrinho vazio.");

                return UseCaseResult<CartItem>.Success(cartItem);
            }
            catch (DomainException ex)
            {
                return UseCaseResult<CartItem>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult<CartItem>.Failure("Ocorreu um erro inesperado: " + ex.Message);
            }
        }
    }
}
