using FastFood.Domain.Entities;

namespace FastFood.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAllCartsAsync();
        Task<Cart?> GetCartByIdAsync(int id);
        Task<Cart?> GetCartByUserIdAsync(Guid id);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task<Cart> InsertCartAsync(Cart cart);
        Task DeleteCartByIdAsync(int id);
    }
}
