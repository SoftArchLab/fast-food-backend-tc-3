//using FastFood.Application.Dtos.CartItem;
//using FastFood.Application.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace FastFood.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class CartItemController : ControllerBase
//    {
//        private readonly ICartItemUseCases _cartItemUseCases;

//        public CartItemController(ICartItemUseCases cartItemUseCases)
//        {
//            _cartItemUseCases = cartItemUseCases;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllCartItems()
//        {
//            var response = await _cartItemUseCases.GetAllCartItemsAsync();

//            if (!response.IsSuccess)
//                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao buscar os itens do carrinho." });

//            return Ok(response);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetCartItem(int id)
//        {
//            var response = await _cartItemUseCases.GetCartItemAsync(id);

//            if (!response.IsSuccess)
//                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao buscar o item do carrinho." });

//            return Ok(response);
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddCartItem([FromBody] EditCartItemDto cartItemDto)
//        {
//            var response = await _cartItemUseCases.AddCartItemAsync(cartItemDto);

//            if (!response.IsSuccess)
//                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao inserir o item do carrinho." });

//            return Ok();
//        }

//        [HttpPut]
//        public async Task<IActionResult> EditCartItem(int id, [FromBody] EditCartItemDto cartItemDto)
//        {
//            var response = await _cartItemUseCases.UpdateCartItemByIdAsync(id, cartItemDto);

//            if (!response.IsSuccess)
//                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao editar o item do carrinho." });

//            return Ok();
//        }

//        [HttpDelete]
//        public async Task<IActionResult> DeleteCartItem(int id)
//        {
//            var response = await _cartItemUseCases.DeleteCartItemByIdAsync(id);

//            if (!response.IsSuccess)
//                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao deletar o item do carrinho." });

//            return Ok();
//        }

//    }
//}
