using FastFood.Domain.Entities;

namespace FastFood.Domain.Interfaces
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetAllCartItemsAsync();
        Task<CartItem> GetCartItemByIdAsync(int id);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task InsertCartItemAsync(CartItem cartItem);
        Task DeleteCartItemByIdAsync(int id);
    }
}
