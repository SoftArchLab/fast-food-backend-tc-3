using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infra.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FastFoodDbContext _context;
        public OrderRepository(FastFoodDbContext context)
        {
            _context = context;
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _context.Orders
                .Where(x => x.Id.Equals(id))
                .ExecuteDeleteAsync();
        }
        public async Task<Order> GetOrderByCartIdAsync(int cartId)
        {
            return await _context.Orders.Include(s => s.Status).Include(c => c.Cart).Include(u => u.User).FirstOrDefaultAsync(x => x.CartId.Equals(cartId)) ?? new Order();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(s => s.Status).Include(c => c.Cart).Include(u => u.User).FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? new Order();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders.Include(s => s.Status).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetInPreparationOrdersAsync()
        {
            return await _context.Orders
                .Where(x => x.Status.Name.Equals(OrderStatusEnum.InPreparation))
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetReadyOrdersAsync()
        {
            return await _context.Orders
                .Where(x => x.Status.Name.Equals(OrderStatusEnum.Ready))
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task SaveOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderByIdAsync(Order order)
        {
            await _context.Orders
                .Where(x => x.Id.Equals(order.Id))
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Status, order.Status)
                    .SetProperty(p => p.CompletionDate, order.CompletionDate)
                    .SetProperty(p => p.Total, order.Total));
        }

        public async Task UpdateOrderStatusByIdAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
