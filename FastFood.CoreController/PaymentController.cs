using FastFood.Application.Dtos.Payment;
using FastFood.Application.Presenters;
using FastFood.Application.UseCases;
using FastFood.DataSource;
using FastFood.Gateway;
using FastFood.Infra.ExternalServices.Interfaces;

namespace FastFood.CoreController
{
    public class PaymentController
    {
        private readonly IDataSource _dataSource;
        private readonly PaymentUseCases _paymentUseCases;
        private readonly PaymentGateway _gateway;
        private readonly PaymentPresenter _presenter;

        public PaymentController(IDataSource dataSource, IMercadoPagoService mercadoPagoService)
        {
            _dataSource = dataSource;
            // Inicializa o gateway de pagamento com o data source e o serviço MercadoPago
            _gateway = new PaymentGateway(
                _dataSource,
                mercadoPagoService
            );
            _paymentUseCases = new PaymentUseCases();
            _presenter = new PaymentPresenter();
        }
        public async Task<UseCaseResult<ResponsePaymentDto>> CreatePaymentAsync(PaymentDto paymentDto)
        {
            var isValid = _paymentUseCases.ValidatePayment(paymentDto);
            if (!isValid)
                throw new Exception("Dados do pagamento inválidos.");

            var payment = await _gateway.GetPaymentByOrderIdAsync(paymentDto.OrderId);

            if (payment.Id != 0)
                throw new Exception("pagamento existente");

            // Chama o gateway para criar o pagamento
            var MPPayment = await _gateway.CreatePaymentAsync(paymentDto);

            var createPaymentResult = _paymentUseCases.CreatePaymentValidate(MPPayment);
            if (!createPaymentResult.IsSuccess)
                throw new Exception(createPaymentResult.Message);

            var response = await _gateway.SavePaymentAsync(MPPayment, paymentDto);
            if (response <= 0)
                throw new Exception("Erro ao salvar o pagamento no banco de dados.");

            return UseCaseResult<ResponsePaymentDto>.Success(_presenter.ToResposePaymentDto(MPPayment));
        }
        public async Task<UseCaseResult<string>> GetStatusPaymentByOrderId(int orderId)
        {
            var response = await _gateway.GetStatusPaymentByOrderId(orderId);
            if (!_paymentUseCases.ValidateStatusName(response))
                throw new Exception("Status do pagamento não encontrado para a ordem informada.");

            return UseCaseResult<string>.Success(response);
        }

        public async Task<UseCaseResult<bool>> HandleNotifications(NotificationsDto notificationsDto)
        {
            var payment = await _gateway.GetPaymentByNotification(notificationsDto.Data);

            _paymentUseCases.SetPaymentStatusApproved(payment);

            await _gateway.UpdatePaymentStatusByPaymentIdAsync(payment);

            return UseCaseResult<bool>.Success(true);

        }
    }
}
