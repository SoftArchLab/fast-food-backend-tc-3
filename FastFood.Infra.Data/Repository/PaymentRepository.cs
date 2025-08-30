using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infra.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly FastFoodDbContext _context;

        public PaymentRepository(FastFoodDbContext context)
        {
            _context = context;
        }

        public async Task DeletePaymentAsync(int id)
        {
            await _context.Payments
                .Where(x => x.Id.Equals(id))
                .ExecuteDeleteAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments.FirstOrDefaultAsync(x => x.Id.Equals(id)) ?? new Payment();
        }

        public async Task<IEnumerable<Payment>> GetPaymentsAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<int> SavePaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return payment.Id;
        }

        public async Task UpdatePaymentByIdAsync(Payment payment)
        {
            await _context.Payments
                .Where(x => x.Id.Equals(payment.Id))
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Method, payment.Method)
                    .SetProperty(p => p.PaymentDate, payment.PaymentDate)
                    .SetProperty(p => p.PaymentStatus, payment.PaymentStatus));
        }

        public async Task UpdatePaymentStatusByPaymentIdAsync(Payment payment)
        {
            await _context.Payments
                .Where(x => x.Id.Equals(payment.Id))
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.PaymentStatusId, (int)payment.PaymentStatus.StatusName));
        }

        public async Task<Payment> GetStatusPaymentByOrderId(int orderId)
        {
            return await _context.Payments.Include(ps => ps.PaymentStatus).Include(o => o.Order).FirstOrDefaultAsync(x => x.OrderId.Equals(orderId)) ?? new Payment();
        }

        public async Task<Payment> GetPaymentByOrderIdAsync(int orderId)
        {
            return await _context.Payments.Include(ps => ps.PaymentStatus).Include(o => o.Order).FirstOrDefaultAsync(x => x.OrderId.Equals(orderId)) ?? new Payment();
        }
    }
}
