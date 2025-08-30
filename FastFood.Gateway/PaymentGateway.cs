using FastFood.Application.Dtos.Payment;
using FastFood.Application.Extensions;
using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Repository;
using FastFood.Infra.ExternalServices.Interfaces;
using MPPayment = MercadoPago.Resource.Payment.Payment;

namespace FastFood.Gateway
{
    public class PaymentGateway
    {
        private readonly IDataSource _dataSource;
        private readonly IMercadoPagoService _mercadoPagoService;
        private readonly IPaymentRepository _paymentRepository;


        public PaymentGateway(IDataSource dataSource, IMercadoPagoService mercadoPagoService)
        {
            _dataSource = dataSource;
            _mercadoPagoService = mercadoPagoService;
            _paymentRepository = new PaymentRepository(_dataSource.GetFastFoodContext());
        }

        public async Task<MPPayment> CreatePaymentAsync(PaymentDto paymentDto)
        {
            try
            {
                var result = await _mercadoPagoService.CreatePaymentAsync(
                    paymentDto.Quantity,
                    paymentDto.Description,
                    paymentDto.PayerEmail,
                    paymentDto.Price,
                    paymentDto.IdEmpotencyKey);

                return result;
            }
            catch (Exception ex)
            {
                if(ex == null)
                    throw new Exception("Erro ao criar pagamento");

                throw new Exception("Erro ao criar pagamento: " + ex.Message);
            }
        }

        public async Task<string> GetStatusPaymentByOrderId(int orderId)
        {
            try
            {
                var payment = await _paymentRepository.GetStatusPaymentByOrderId(orderId);
                if (payment == null)
                {
                    throw new Exception("Payment not found for the given order ID.");
                }
                return payment.PaymentStatus.StatusName.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving payment status: " + ex.Message);
            }

        }

        public async Task<int> SavePaymentAsync(MPPayment payment, PaymentDto paymentDto)
        {
            try
            {
                return await _paymentRepository.SavePaymentAsync(
                    paymentDto.ToEntity("pix", DateTime.Now, payment.Id, paymentDto.OrderId, (int)PaymentStatusEnum.Pending));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar o pagamento: " + ex.Message);
            }
        }
        public async Task UpdatePaymentStatusByPaymentIdAsync(Payment payment)
        {
            try
            {
                await _paymentRepository.UpdatePaymentStatusByPaymentIdAsync(payment);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar o pagamento: " + ex.Message);
            }
        }

        public async Task<Payment> GetPaymentByOrderIdAsync(int orderId)
        {
            try
            {
                return await _paymentRepository.GetPaymentByOrderIdAsync(orderId);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar o pagamento: " + ex.Message);
            }
        }

        public async Task GetPaymentStatusByIdAsync(string notificationsDtoId)
        {
            // var paymentStatus = await _mercadoPagoService.
            await Task.CompletedTask;
        }

        public async Task<Payment> GetPaymentByNotification(NotificationDataDto dataDto)
        {
            try
            {
                return await _paymentRepository.GetPaymentByOrderIdAsync(Int32.Parse(dataDto.Id));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar o pagamento: " + ex.Message);
            }
        }
    }
}
