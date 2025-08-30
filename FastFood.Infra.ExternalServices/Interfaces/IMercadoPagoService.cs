using MercadoPago.Resource.Payment;

namespace FastFood.Infra.ExternalServices.Interfaces;

public interface IMercadoPagoService
{
    Task<Payment> CreatePaymentAsync(int quantity, string description, string payerEmail, decimal price, Guid idEmpotencyKey);
}