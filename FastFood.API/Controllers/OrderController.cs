using FastFood.Application.Helpers;
using FastFood.DataSource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = AuthorizeRoles.AllRoles)]
    public class OrderController : ControllerBase
    {
        private readonly IDataSource _dataSource;
        private readonly string orderStatusInPreparation = "InPreparation";
        private readonly string orderStatusReady = "Ready";

        public OrderController(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [EndpointSummary("Admin - Obtem todas as ordens")]
        [HttpGet("/Order/GetOrders/")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> GetOrders()
        {
            var coreController = new CoreController.OrderController(_dataSource);
            var response = await coreController.GetAllUsers();

            return Ok(response);
        }

        [EndpointSummary("Todos perfis - Obtem ordem por ID")]
        [HttpGet("/Order/GetOrderById/{id}")]
        [Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var coreController = new CoreController.OrderController(_dataSource);
            var response = await coreController.GetOrderById(id);

            return Ok(response);
        }

        [EndpointSummary("Admin - Atualiza status da ordem")]
        [HttpPut("/Order/UpdateOrderStatus/{id}")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> UpdateOrderStatus(int id)
        {
            var coreController = new CoreController.OrderController(_dataSource);
            var response = await coreController.UpdateOrderStatusById(id);

            return Ok(response);
        }

        [EndpointSummary("Todos perfis - Obtem ordens com status 'Em preparação'")]
        [HttpGet("/Order/GetInPreparationOrders/")]
        [Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> GetInPreparationOrders()
        {
            var coreController = new CoreController.OrderController(_dataSource);
            var response = await coreController.GetOrdersFromOrderStatus(orderStatusInPreparation);

            return Ok(response);
        }

        [EndpointSummary("Todos perfis - Obtem ordens com status 'Pronto'")]
        [HttpGet("/Order/GetReadyOrders/")]
        [Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> GetReadyOrders()
        {
            var coreController = new CoreController.OrderController(_dataSource);
            var response = await coreController.GetOrdersFromOrderStatus(orderStatusReady);

            return Ok(response);
        }

    }
}
