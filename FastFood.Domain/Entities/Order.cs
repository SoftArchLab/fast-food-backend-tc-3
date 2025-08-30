using FastFood.Domain.Enums;

namespace FastFood.Domain.Entities
{
    public class Order
    {
        #region Properties

        public int Id { get; private set; }
        public Guid UserId { get; private set; }
        public int CartId { get; private set; }
        public int PaymentId { get; private set; }
        public decimal Total { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? CompletionDate { get; private set; }
        public int OrderStatusId { get; private set; }
        public virtual OrderStatus Status { get; private set; }
        public virtual User User { get; private set; }
        public virtual Cart Cart { get; private set; }
        public virtual Payment Payment { get; private set; }

        #endregion

        #region Construtor

        public Order() {}

        public Order(Guid userId, int cartId, decimal total, DateTime createdDate, int orderStatusId)
        {
            UserId = userId;
            CartId = cartId;
            Total = total;
            CreatedDate = createdDate;
            OrderStatusId = orderStatusId;
        }

        #endregion

        #region Methods

        public static Order CreateOrder(Guid userId, int cartId, decimal total, int orderStatusId = (int)OrderStatusEnum.Received) 
        { 
            if (!ValidadeCreateOrder(userId, cartId, total))
                throw new ArgumentException("Pedido inválido, erro ao criar o pedido.");
            
            Order order = new Order(
                userId, 
                cartId, 
                total, 
                DateTime.Now,
                orderStatusId
            );

            return order;
        }

        public Order EditOrder(Guid userId, int cartId, decimal total, DateTime createdDate, DateTime? completionDate, OrderStatus status)
        {
            if (!ValidadeEditOrder(userId, cartId, total, createdDate, completionDate, status.Name))
                throw new ArgumentException("Pedido inválido, erro ao atualizar o pedido.");

            UserId = userId;
            CartId = cartId;
            Total = total;
            CreatedDate = createdDate;
            CompletionDate = completionDate;
            Status = status;

            return this;
        }

        public void UpdateStatus() 
        {
            OrderStatusId = (int)GetNextStatus();
        }

        public void FinalizeOrder() 
        {
            if (!ValidadeStatus(OrderStatusEnum.Finished))
                throw new ArgumentException("Impossivel finalizar o pedido a partir do status atual.");

            Status = OrderStatus.CreateOrderStatus(OrderStatusEnum.Finished);
            CompletionDate = DateTime.Now;
        }

        private OrderStatusEnum GetNextStatus()
        {
            switch (Status.Name)
            {   
                case OrderStatusEnum.Received:
                    return OrderStatusEnum.InPreparation;
                case OrderStatusEnum.InPreparation:
                    return OrderStatusEnum.Ready;
                case OrderStatusEnum.Ready:
                    return OrderStatusEnum.Finished;
                default:
                    return OrderStatusEnum.Finished;
            }
        }

        #endregion

        #region Validations

        private static bool ValidadeCreateOrder(Guid userId, int cartId, decimal total) 
        {
            if (userId == Guid.Empty) return false;
            else if (cartId <= 0) return false;
            else if (total <= 0) return false;

            return true;
        }

        private bool ValidadeEditOrder(Guid userId, int cartId, decimal total, DateTime createdDate, DateTime? completionDate, OrderStatusEnum status)
        {
            if (userId == Guid.Empty) return false;
            else if (cartId <= 0) return false;
            else if (total <= 0) return false;
            else if (!ValidadeStatus(status)) return false;

            return true;
        }

        private bool ValidadeStatus(OrderStatusEnum newStatus) 
        {
            switch (newStatus)
            {
                case OrderStatusEnum.Finished:
                    if (!Status.Equals(OrderStatusEnum.Ready))
                        return false;
                    return true;
                case OrderStatusEnum.Ready:
                    if (!Status.Equals(OrderStatusEnum.InPreparation))
                        return false;
                    return true;
                case OrderStatusEnum.InPreparation:
                    if (!Status.Equals(OrderStatusEnum.Received))
                        return false;
                    return true;
                case OrderStatusEnum.Received:
                    if (!Status.Equals(OrderStatusEnum.Received))
                        return false;
                    return true;
                default: 
                    return false;
            }
        }

        #endregion
    }
}
