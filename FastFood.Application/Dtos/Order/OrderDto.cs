using FastFood.Domain.Entities;

namespace FastFood.Application.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int CartId { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
