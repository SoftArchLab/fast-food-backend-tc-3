using FastFood.Application.Dtos.Payment;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Exceptions;
using FastFood.Domain.Interfaces;
using FastFood.Infra.ExternalServices.Interfaces;
using MPPayment = MercadoPago.Resource.Payment.Payment;

namespace FastFood.Application.UseCases
{
    public class PaymentUseCases : IPaymentUseCases
    {
        private readonly IMercadoPagoService _mercadoPagoService;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentUseCases() { }


        public UseCaseResult<MPPayment> CreatePaymentValidate(MPPayment mpPayment)
        {
            if (mpPayment == null)
                UseCaseResult<MPPayment>.Failure("Dados do pagamento não informados.");
            else if (mpPayment.Id == 0 || mpPayment.Id == null)
                UseCaseResult<MPPayment>.Failure("Id do pagamento não informado.");
            else if (mpPayment.Status == null || string.IsNullOrWhiteSpace(mpPayment.Status.ToString()))
                UseCaseResult<MPPayment>.Failure("Status do pagamento não informado.");
            else if (mpPayment.PointOfInteraction.TransactionData.TicketUrl == null || string.IsNullOrWhiteSpace(mpPayment.PointOfInteraction.TransactionData.TicketUrl))
                UseCaseResult<MPPayment>.Failure("URL do ticket de pagamento não informada.");

            return UseCaseResult<MPPayment>.Success(mpPayment);
        }

        public void SetPaymentStatusApproved(Payment? payment)
        {
            if(payment == null)
                throw new DomainException("Dados do pagamento não informados.");
            else if (payment.Id == 0 || payment.Id == null)
                throw new DomainException("Id do pagamento não informado.");

            var currentStatus = payment.PaymentStatus;
            if (currentStatus == null)
                throw new DomainException("Status do pagamento não informado.");

            if (currentStatus.StatusName == PaymentStatusEnum.Pending || currentStatus.StatusName.Equals(PaymentStatusEnum.InProcess))
                payment.PaymentStatus = PaymentStatus.CreatePaymentStatus(PaymentStatusEnum.Approved);
        }

        public bool ValidatePayment(PaymentDto paymentDto)
        {
            if (paymentDto == null)
                throw new DomainException("Dados do pagamento não informados.");
            else if (string.IsNullOrEmpty(paymentDto.PayerEmail) || string.IsNullOrWhiteSpace(paymentDto.PayerEmail))
                throw new DomainException("E-mail do pagador não informado.");
            else if (paymentDto.Price <= 0)
                throw new DomainException("Valor do pagamento inválido.");
            else if (paymentDto.Quantity <= 0)
                throw new DomainException("Quantidade inválida.");
            return true;
        }

        public bool ValidateStatusName(string statusName)
        {
            if (string.IsNullOrEmpty(statusName) || string.IsNullOrWhiteSpace(statusName))
                throw new DomainException("Status do pagamento inválido.");
            return true;
        }

        public async Task<UseCaseResult<string>> GetStatusPaymentByOrderId(int orderId)
        {
            try
            {
                string statusName = string.Empty;
                var result = await _paymentRepository.GetStatusPaymentByOrderId(orderId);

                if (result != null)
                    statusName = result.PaymentStatus.StatusName.ToString();

                if (string.IsNullOrEmpty(statusName) || string.IsNullOrWhiteSpace(statusName))
                    throw new DomainException("Status não encontrado.");

                return UseCaseResult<string>.Success(statusName);
            }
            catch (DomainException ex)
            {
                return UseCaseResult<string>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult<string>.Failure("Erro ao consultar o status do pagamento: " + ex.Message);
            }
        }

    }
}
