using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infra.Data.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly FastFoodDbContext _context;

        public CartItemRepository(FastFoodDbContext context)
        {
            _context = context;
        }

        public async Task DeleteCartItemByIdAsync(int id)
        {
            await _context.CartItems
                .Where(x => x.Id.Equals(id))
                .ExecuteDeleteAsync();
        }

        public async Task<List<CartItem>> GetAllCartItemsAsync()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<CartItem> GetCartItemByIdAsync(int id)
        {
            return await _context.CartItems.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task InsertCartItemAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            await _context.CartItems
                .Where(x => x.Id.Equals(cartItem.Id))
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.ProductId, cartItem.ProductId)
                    .SetProperty(p => p.Quantity, cartItem.Quantity));
        }
    }
}
