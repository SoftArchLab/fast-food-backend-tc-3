using FastFood.Application.Dtos.Payment;
using FastFood.Application.Helpers;
using FastFood.DataSource;
using FastFood.Infra.ExternalServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : Controller
    {
        private readonly IDataSource _dataSource;
        private readonly CoreController.PaymentController _controller;

        public PaymentController(IDataSource dataSource, IMercadoPagoService mercadoPagoService)
        {
            _dataSource = dataSource;
            _controller = new CoreController.PaymentController(_dataSource, mercadoPagoService);

        }

        [EndpointSummary("Todos perfis - Gera QR de pagamento do pedido via MP")]
        [HttpPost("CreatePayment")]
        [Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDto paymentDto)
        {
            var repsonse = await _controller.CreatePaymentAsync(paymentDto);

            if (!repsonse.IsSuccess)
                return StatusCode(500, new { message = repsonse.Message ?? "Ocorreu um erro inesperado ao criar pagamento." });

            return Ok(repsonse.Data);
        }

        [EndpointSummary("Todos perfis - Retorna o status do pagamento da ordem solicitada.")]
        [HttpGet("GetStatusPaymentByOrderId/{orderId}")]
        [Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> GetStatusPaymentByOrderId(int orderId)
        {
            var response = await _controller.GetStatusPaymentByOrderId(orderId);

            if (!response.IsSuccess)
                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao buscar o status do pagamento da ordem." });

            return Ok(response);
        }
        
        [EndpointSummary("Todos perfis - Lida com notificações de pagamento dos pedidos.")]
        [HttpPost("Webhook")]
        // [Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> HandleNotifications(NotificationsDto notificationsDto)
        {
            var response = await _controller.HandleNotifications(notificationsDto);

            if (!response.IsSuccess)
                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao buscar o status do pagamento da ordem." });

            return Ok();
        }
    }
}
