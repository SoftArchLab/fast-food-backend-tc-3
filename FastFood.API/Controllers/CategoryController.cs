using FastFood.Application.Dtos.Category;
using FastFood.Application.Helpers;
using FastFood.DataSource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly IDataSource _dataSource;
        private readonly CoreController.CategoryController _controller;

        public CategoryController(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _controller = new CoreController.CategoryController(_dataSource);
        }

        #region AllRoles
        [EndpointSummary("Todos Perfis - Obtem todos as categorias")]
        [HttpGet("/Category/GetCategories/")]
        //[Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _controller.GetCategories();

            return Ok(response);
        }

        [EndpointSummary("Todos Perfis - Obtem categoria por ID")]
        [HttpGet("/Category/GetCategoryById/{id}")]
        [Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> GetCategory(int id)
        {
            var response = await _controller.GetCategoryById(id);

            return Ok(response);
        }
        #endregion

        #region ADMIN
        [EndpointSummary("Admin - Adiciona categoria")]
        [HttpPost("/Category/AddCategory/")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> Save([FromBody] SaveCategoryDto categoryDto)
        {
            var response = await _controller.SaveCategory(categoryDto);

            if (!response.IsSuccess)
                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao salvar a categoria." });

            return Ok(response);
        }

        [EndpointSummary("Admin - Atualiza categoria")]
        [HttpPut("/Category/UpdateCategory/{id}")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> Edit(int id, [FromBody] SaveCategoryDto categoryDto)
        {
            var response = await _controller.UpdateCategoryById(id, categoryDto);

            if (!response.IsSuccess)
                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao atualizar a categoria." });

            return Ok(response);
        }


        [EndpointSummary("Admin - Remove categoria")]
        [HttpDelete("/Category/RemoveCategory/{id}")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _controller.DeleteCategoryById(id);

            if (!response.IsSuccess)
                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao excluir a categoria." });

            return Ok(response);
        }
        #endregion
    }
}
