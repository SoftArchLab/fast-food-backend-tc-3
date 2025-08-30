using FastFood.Domain.Entities;

namespace FastFood.Domain.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrdersAsync();
        public Task<Order> GetOrderByCartIdAsync(int cartId);
        public Task<Order> GetOrderByIdAsync(int id);
        public Task<IEnumerable<Order>> GetInPreparationOrdersAsync();
        public Task<IEnumerable<Order>> GetReadyOrdersAsync();
        public Task SaveOrderAsync(Order order);
        public Task UpdateOrderByIdAsync(Order order);
        public Task UpdateOrderStatusByIdAsync(Order order);
        public Task DeleteOrderAsync(int id);
    }
}
