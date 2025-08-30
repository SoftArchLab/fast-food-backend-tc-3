using FastFood.Application.Dtos.CartItem;
using FastFood.Application.Helpers;
using FastFood.Application.Interfaces;
using FastFood.DataSource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IDataSource _dataSource;

        public CartController(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [EndpointSummary("Guest e Customer - Obtem carrinho usando o ID do usuário")]
        [HttpGet("/Cart/GetUserCart/{userId}")]
        [Authorize(Roles = AuthorizeRoles.GuestAndCustomerRoles)]
        public async Task<IActionResult> GetUserCart(Guid userId)
        {
            var controller = new CoreController.CartController(_dataSource);
            var response = await controller.GetUserCart(userId);

            return Ok(response);
        }

        [EndpointSummary("Guest e Customer - Adiciona item do carrinho")]
        [HttpPost("/Cart/AddCartItem/")]
        [Authorize(Roles = AuthorizeRoles.GuestAndCustomerRoles)]
        public async Task<IActionResult> AddCartItem([FromBody] CartItemDto cartDto)
        {
            var controller = new CoreController.CartController(_dataSource);
            var response = await controller.AddCartItem(cartDto);

            return Ok(response);
        }

        [EndpointSummary("Guest e Customer - Remove item do carrinho")]
        [HttpDelete("/Cart/RemoveCartItem/")]
        [Authorize(Roles = AuthorizeRoles.GuestAndCustomerRoles)]
        public async Task<IActionResult> RemoveCartItem([FromBody] CartItemDto cartDto)
        {
            var controller = new CoreController.CartController(_dataSource);
            var response = await controller.RemoveCartItem(cartDto);

            return Ok(response);
        }

        [EndpointSummary("Guest e Customer - Gerar ordem com ID cart")]
        [HttpPost("/Cart/GenerateOrderFromCart/{id}")]
        [Authorize(Roles = AuthorizeRoles.GuestAndCustomerRoles)]
        public async Task<IActionResult> GenerateOrderFromCart(int id)
        {
            var controller = new CoreController.OrderController(_dataSource);
            var response = await controller.GenerateOrderFromCart(id);

            return Ok(response);
        }
    }
}
