using FastFood.Application.Dtos.Payment;
using MercadoPago.Resource.Payment;

namespace FastFood.Application.Presenters
{
    public class PaymentPresenter
    {
        public PaymentPresenter() { }

        public ResponsePaymentDto ToResposePaymentDto(Payment payment)
        {
            return new ResponsePaymentDto
            {
                Status = payment.Status,
                TicketUrl = payment.PointOfInteraction.TransactionData.TicketUrl
            };
        }
    }
}
