using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Repository;

namespace FastFood.Gateway
{
    public class OrderGateway
    {
        private readonly IDataSource _dataSource;
        private readonly IOrderRepository _orderRepository;

        public OrderGateway(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _orderRepository = new OrderRepository(_dataSource.GetFastFoodContext());
        }
        public Order CreateOrder(Cart cart)
        {
            return Order.CreateOrder(cart.UserId, cart.Id, cart.Subtotal);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                return await _orderRepository.GetOrdersAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving orders.", ex);
            }
        }

        public async Task<Order> GetOrderByCartId(int cartId)
        {
            try
            {
                return await _orderRepository.GetOrderByCartIdAsync(cartId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving order.", ex);
            }
        }
        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                return await _orderRepository.GetOrderByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving order.", ex);
            }
        }
        public async Task<IEnumerable<Order>> GetOrdersFromOrderStatus(string status)
        {
            try
            {
                IEnumerable<Order> orders = new List<Order>();

                var orderStatus = (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), status, true);
                
                switch (orderStatus)
                {
                    case OrderStatusEnum.InPreparation:
                        orders = await _orderRepository.GetInPreparationOrdersAsync();
                        break;
                    case OrderStatusEnum.Ready:
                        orders = await _orderRepository.GetReadyOrdersAsync();
                        break;
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving order.", ex);
            }
        }
        public async Task InsertOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.SaveOrderAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving order.", ex);
            }
        }
        public async Task UpdateOrderByIdAsync(Order order)
        {
            try
            {
                await _orderRepository.UpdateOrderByIdAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving order.", ex);
            }
        }
        public async Task UpdateOrderStatusByIdAsync(Order order)
        {
            try
            {
                await _orderRepository.UpdateOrderStatusByIdAsync(order);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving order.", ex);
            }
        }
        public async Task DeleteOrderByIdAsync(int id)
        {
            try
            {
                await _orderRepository.DeleteOrderAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving order.", ex);
            }
        }
    }
}
