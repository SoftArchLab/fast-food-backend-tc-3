using FastFood.Domain.Enums;
using FastFood.Domain.Exceptions;

namespace FastFood.Domain.Entities
{
    public class PaymentStatus
    {
        public int Id { get; private set; }
        public PaymentStatusEnum StatusName { get; private set; }
        public virtual Payment Payment { get; private set; }

        public PaymentStatus() { }

        public static PaymentStatus CreatePaymentStatus(PaymentStatusEnum status)
        {

            if (!Enum.IsDefined(typeof(PaymentStatusEnum), status))
                throw new DomainException("Status de pagamento inválido.");

            return new PaymentStatus
            {
                StatusName = status
            };
        }

        public static PaymentStatus Create(int paymentId)
        {
            return new PaymentStatus(paymentId);
        }

        private PaymentStatus(int paymentId)
        {
            Id = paymentId;
            StatusName = PaymentStatusEnum.Pending;
        }
    }
}
