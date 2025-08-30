using FastFood.Infra.ExternalServices.Interfaces;
using MercadoPago.Client;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;
using Microsoft.Extensions.Configuration;

namespace FastFood.Infra.ExternalServices
{
    public class MercadoPagoService : IMercadoPagoService
    {
        private readonly IConfiguration _configuration;

        public MercadoPagoService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Pega o token do appsettings ou do secret manager
            MercadoPagoConfig.AccessToken = _configuration["MercadoPago:AccessToken"];
        }

        // public async Task<Payment> GetPaymentStatusByIdAsync(int id)
        // {
        //     var requestOptions = BuildClient(out var client, idEmpotencyKey);
        //
        //     var paymentItem = BuildPaymentItemRequest(quantity, description, price);
        //
        //     var request = new PaymentCreateRequest
        //     {
        //         Payer = BuildPaymentPayerRequest(payerEmail),
        //         PaymentMethodId = "pix",
        //         TransactionAmount = price,
        //         AdditionalInfo = BuildPaymentAdditionalInfoRequest(paymentItem)
        //     };
        //     
        //     return await client.GetAsync(request, requestOptions);
        // }
        
        public async Task<Payment> CreatePaymentAsync(int quantity, string description, string payerEmail, decimal price, Guid idEmpotencyKey)
        {
            var requestOptions = BuildClient(out var client, idEmpotencyKey);

            var paymentItem = BuildPaymentItemRequest(quantity, description, price);

            var request = new PaymentCreateRequest
            {
                Payer = BuildPaymentPayerRequest(payerEmail),
                PaymentMethodId = "pix",
                TransactionAmount = price,
                AdditionalInfo = BuildPaymentAdditionalInfoRequest(paymentItem)
            };
            
            return await client.CreateAsync(request, requestOptions);
        }

        private static RequestOptions BuildClient(out PaymentClient client, Guid idEmpotencyKey)
        {
            var requestOptions = new RequestOptions();
            requestOptions.CustomHeaders.Add("x-idempotency-key", idEmpotencyKey.ToString());
            client = new PaymentClient();
            return requestOptions;
        }

        private static PaymentPayerRequest BuildPaymentPayerRequest(string payerEmail)
        {
            var paymentPayerRequest = new PaymentPayerRequest
            {
                Email = payerEmail
            };
            return paymentPayerRequest;
        }

        private static PaymentItemRequest BuildPaymentItemRequest(int quantity, string description, decimal price)
        {
            var item = new PaymentItemRequest
            {
                Title = description,
                Description = description,
                Quantity = quantity,
                UnitPrice = price,
                EventDate = DateTime.Now
            };
            return item;
        }

        private static PaymentAdditionalInfoRequest BuildPaymentAdditionalInfoRequest(PaymentItemRequest item)
        {
            var additionalInfo = new PaymentAdditionalInfoRequest
            {
                Items = new List<PaymentItemRequest> { item }
            };
            return additionalInfo;
        }
    }
}