namespace FastFood.Application.Dtos.CartItem
{
    public class CartItemDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
    }
}
