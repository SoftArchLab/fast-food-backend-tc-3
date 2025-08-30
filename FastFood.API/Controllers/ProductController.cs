using FastFood.Application.Dtos.Product;
using FastFood.Application.Helpers;
using FastFood.Application.Interfaces;
using FastFood.DataSource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        protected readonly IDataSource _dataSource;
        protected readonly IProductUseCases _productUseCases;

        public ProductController(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [EndpointSummary("Todos Perfis - Obtem todos os produtos")]
        [HttpGet("/Product/GetProducts/")]
        [Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> GetAllProducts()
        {
            var controller = new CoreController.ProductController(_dataSource);
            var response = await controller.GetProducts();

            return Ok(response);
        }

        [EndpointSummary("Todos Perfis - Obtem produto por ID")]
        [HttpGet("/Product/GetProductById/{id}")]
        [Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> GetProductById(int id)
        {
            var controller = new CoreController.ProductController(_dataSource);
            var response = await controller.GetProductById(id);

            return Ok(response);
        }

        [EndpointSummary("Admin - Adiciona produto")]
        [HttpPost("/Product/AddProduct/")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> AddProduct([FromBody] UpdateProductDto productDto)
        {
            var controller = new CoreController.ProductController(_dataSource);
            var response = await controller.AddProduct(productDto);

            return Ok(response);
        }

        [EndpointSummary("Admin - Atualiza produto")]
        [HttpPut("/Product/UpdateProduct/{id}")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto productDto)
        {
            var controller = new CoreController.ProductController(_dataSource);
            var response = await controller.EditProduct(id, productDto);

            return Ok(response);
        }

        [EndpointSummary("Admin - Deleta produto")]
        [HttpDelete("/Product/RemoveProduct/{id}")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var controller = new CoreController.ProductController(_dataSource);
            var response = await controller.DeleteProduct(id);

            return Ok(response);
        }
    }
}
