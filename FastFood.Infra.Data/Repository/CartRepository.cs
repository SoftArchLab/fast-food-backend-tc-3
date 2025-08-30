using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infra.Data.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly FastFoodDbContext _context;

        public CartRepository(FastFoodDbContext context)
        {
            _context = context;
        }

        public async Task DeleteCartByIdAsync(int id)
        {
            await _context.Carts
                .Where(x => x.Id.Equals(id))
                .ExecuteDeleteAsync();
        }

        public async Task<List<Cart>> GetAllCartsAsync()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart?> GetCartByIdAsync(int id)
        {
            return await _context.Carts
                .AsNoTracking()
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.IsFinished);
        }

        public async Task<Cart?> GetCartByUserIdAsync(Guid id)
        {
            return await _context.Carts
                //.AsNoTracking()
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.UserId == id && !x.IsFinished);
        }

        public async Task<Cart> InsertCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return await _context.Carts.OrderBy(x => x.Id).LastAsync();
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync(); 
            return await _context.Carts.OrderBy(x => x.Id).LastAsync();
        }
    }
}
