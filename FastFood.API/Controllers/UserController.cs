using FastFood.Application.Dtos.User;
using FastFood.Application.Helpers;
using FastFood.Application.Interfaces;
using FastFood.DataSource;
using FastFood.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Net;
using System.Xml.Linq;

namespace FastFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IDataSource _dataSource;

        public UserController(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        #region Methods

        [EndpointSummary("Admin - Obtem todos os usuários")]
        [HttpGet("/User/GetUsers/")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> GetAllUsers()
        {
            var coreController = new CoreController.UserController(_dataSource);
            var response = await coreController.GetAllUsers();

            return Ok(response);
        }

        [EndpointSummary("Todos perfis - Obtem usuários por ID")]
        [HttpGet("/User/GetUserById/{id}")]
        [Authorize(Roles = AuthorizeRoles.AllRoles)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var coreController = new CoreController.UserController(_dataSource);
            var response = await coreController.GetUserById(id);

            return Ok(response);
        }

        [EndpointSummary("Guest e Customer - Insere usuário guest e customer")]
        [HttpPost("/User/AddCustomerUser/")]
        [Authorize(Roles = AuthorizeRoles.GuestAndCustomerRoles)]
        public async Task<IActionResult> AddCustomerUser([FromBody] CreateUserDto p_Data)
        {
            var coreController = new CoreController.UserController(_dataSource);
            var response = await coreController.AddUser(p_Data, UserRole.Customer);

            return Ok(response);
        }

        [EndpointSummary("Admin - Insere usuário admin")]
        [HttpPost("/User/AddAdminUser/")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> AddAdminUser([FromBody] CreateUserDto p_Data)
        {
            var coreController = new CoreController.UserController(_dataSource);
            var response = await coreController.AddUser(p_Data, UserRole.Admin);

            return Ok(response);
        }

        [EndpointSummary("Admin e Customer - Edita usuário")]
        [HttpPut("/User/UpdateUser/{id}")]
        [Authorize(Roles = AuthorizeRoles.AdminAndCustomerRoles)]
        public async Task<IActionResult> EditUser(Guid id, [FromBody] UpdateUserDto p_Data)
        {
            var coreController = new CoreController.UserController(_dataSource);
            var response = await coreController.EditUser(id, p_Data);

            return Ok(response);
        }

        [EndpointSummary("Admin - Deleta usuário")]
        [HttpDelete("/User/RemoveUser/{id}")]
        [Authorize(Roles = AuthorizeRoles.Admin)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var coreController = new CoreController.UserController(_dataSource);
            var response = await coreController.DeleteUser(id);

            return Ok(response);
        }

        #endregion
    }
}
