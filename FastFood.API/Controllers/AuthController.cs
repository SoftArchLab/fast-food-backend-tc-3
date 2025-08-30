using FastFood.Application.Dtos.User;
using FastFood.Application.Interfaces;
using FastFood.DataSource;
using FastFood.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IDataSource _dataSource;
        private readonly CoreController.UserController _controller;

        public AuthController(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _controller = new CoreController.UserController(dataSource);
        }

        [HttpPost("/Login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateUserDto authDto)
        {
            var response = await _controller.AuthenticateAsync(authDto);

            if (response.Data == null)
                return Unauthorized(new { message = "Falha na autenticação" });
            else if (!response.IsSuccess)
                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao autenticar o usuário." });

            return Ok(response);
        }
    }
}
