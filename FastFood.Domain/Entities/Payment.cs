namespace FastFood.Domain.Entities
{
    public class Payment
    {
        #region Properties

        public int Id { get; private set; }
        public long? PaymentIdMP { get; private set; }
        public string Price { get; private set; }
        public string Method { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public int OrderId { get; private set; }
        public int PaymentStatusId { get; private set; }

        public virtual Order Order { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }

        #endregion

        public Payment(string price, string method, DateTime paymentDate, long? paymentIdMP, int orderId, int paymentStatusId)
        {
            Price = price;
            Method = method;
            PaymentDate = paymentDate;
            PaymentIdMP = paymentIdMP;
            OrderId = orderId;
            PaymentStatusId = paymentStatusId;
        }

        public Payment() { }
    }
}
