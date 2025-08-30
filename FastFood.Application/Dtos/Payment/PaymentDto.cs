namespace FastFood.Application.Dtos.Payment
{
    public class PaymentDto
    {
        public int OrderId { get; set; }
        public Guid IdEmpotencyKey { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string PayerEmail { get; set; }
        public decimal Price { get; set; }

    }
}
