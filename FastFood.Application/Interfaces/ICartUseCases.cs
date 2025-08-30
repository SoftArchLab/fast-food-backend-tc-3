using FastFood.Application.Dtos.Cart;
using FastFood.Application.Dtos.CartItem;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;


namespace FastFood.Application.Interfaces
{
    public interface ICartUseCases
    {
        Task<UseCaseResult<Guid>> ValidateUserId(Guid id);
        Task<UseCaseResult<int>> ValidateCartId(int id);
        CartItem AddOrUpdateCartItemInCart(Cart cart, CartItem cartItem);
    }
}
