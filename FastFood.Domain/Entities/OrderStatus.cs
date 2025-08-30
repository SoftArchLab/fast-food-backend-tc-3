using FastFood.Domain.Enums;
using FastFood.Domain.Exceptions;

namespace FastFood.Domain.Entities
{
    public class OrderStatus
    {
        public int Id { get; private set; }
        public OrderStatusEnum Name { get; private set; }
        public virtual Order Order { get; private set; }

        public OrderStatus() { }

        public static OrderStatus CreateOrderStatus(OrderStatusEnum status)
        {

            if (!Enum.IsDefined(typeof(OrderStatusEnum), status))
                throw new DomainException("Status de pedido inválido.");

            return new OrderStatus
            {
                Name = status
            };
        }

        public void SetOrderStatus(OrderStatusEnum status)
        {
            if (!Enum.IsDefined(typeof(OrderStatusEnum), status))
                throw new DomainException("Status de pedido inválido.");
            Name = status;
        }
    }
}
