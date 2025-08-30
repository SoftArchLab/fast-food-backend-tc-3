using FastFood.Application.Dtos.CartItem;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FastFood.Domain.Exceptions;
using FastFood.Domain.Interfaces;

namespace FastFood.Application.UseCases
{
    public class CartUseCases : ICartUseCases
    {
        public CartUseCases() {}
        public CartItem AddOrUpdateCartItemInCart(Cart cart, CartItem cartItem)
        {
            try
            {
                return cart.AddOrUpdateCartItemInCart(cartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado: " + ex.Message);
            }
        }
        public async Task<UseCaseResult<int>> ValidateCartId(int id)
        {
            try
            {
                if (id <= 0)
                    throw new DomainException("ID do carrinho inválido.");

                return UseCaseResult<int>.Success(id);
            }
            catch (DomainException ex)
            {
                return UseCaseResult<int>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult<int>.Failure("Ocorreu um erro inesperado: " + ex.Message);
            }
        }
        public async Task<UseCaseResult<Guid>> ValidateUserId(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new DomainException("ID usuário inválido.");

                return UseCaseResult<Guid>.Success(id);
            }
            catch (DomainException ex)
            {
                return UseCaseResult<Guid>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult<Guid>.Failure("Ocorreu um erro inesperado: " + ex.Message);
            }
        }
    }
}
