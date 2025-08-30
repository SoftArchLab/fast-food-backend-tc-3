using FastFood.Domain.Entities;

namespace FastFood.Domain.Interfaces
{
    public interface IPaymentRepository
    {
        public Task<IEnumerable<Payment>> GetPaymentsAsync();
        public Task<Payment> GetPaymentByIdAsync(int id);
        public Task<int> SavePaymentAsync(Payment payment);
        public Task UpdatePaymentByIdAsync(Payment payment);
        public Task UpdatePaymentStatusByPaymentIdAsync(Payment payment);
        public Task DeletePaymentAsync(int id);
        public Task<Payment> GetStatusPaymentByOrderId(int orderId);
        public Task<Payment> GetPaymentByOrderIdAsync(int orderId);
    }
}
