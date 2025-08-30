using FastFood.Application.UseCases;
using MercadoPago.Resource.Payment;

namespace FastFood.Application.Interfaces
{
    public interface IPaymentUseCases
    {
        UseCaseResult<Payment> CreatePaymentValidate(Payment mpPayment);
        Task<UseCaseResult<string>> GetStatusPaymentByOrderId(int orderId);
    }
}
