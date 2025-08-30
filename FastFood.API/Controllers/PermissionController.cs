//using FastFood.Application.Dtos;
//using FastFood.Application.Helpers;
//using FastFood.Application.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace FastFood.Controllers
//{
//    [ApiController]
//    [Authorize(Roles = AuthorizeRoles.Admin)]
//    [Route("[controller]")]
//    public class PermissionController : ControllerBase
//    {
//        private readonly IPermissionUseCases _permissionUseCases;

//        public PermissionController(IPermissionUseCases permissionUseCases)
//        {
//            _permissionUseCases = permissionUseCases;
//        }

//        #region Methods

//        [HttpGet]
//        public async Task<IActionResult> GetAllPermissions()
//        {
//            var response = await _permissionUseCases.GetAllPermissionsAsync();

//            if (!response.IsSuccess)
//                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao buscar as permissões." });

//            return Ok(response);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetPermissionById(int id)
//        {
//            var response = await _permissionUseCases.GetPermissionByIdAsync(id);
//            if (!response.IsSuccess)
//                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao buscar a permissão." });

//            return Ok(response);
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddPermission([FromBody] EditPermissionDto p_Data)
//        {
//            var response = await _permissionUseCases.AddPermissionAsync(p_Data);

//            if (!response.IsSuccess)
//                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao inserir a permissão." });

//            return Ok(response);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> EditPermission(int id, [FromBody] EditPermissionDto p_Data)
//        {
//            var response = await _permissionUseCases.UpdatePermissionByIdAsync(id, p_Data);

//            if (!response.IsSuccess)
//                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao editar a permissão." });

//            return Ok(response);
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePermission(int id)
//        {
//            var response = await _permissionUseCases.DeletePermissionByIdAsync(id);

//            if (!response.IsSuccess)
//                return StatusCode(500, new { message = response.Message ?? "Ocorreu um erro inesperado ao deletar a permissão." });

//            return Ok(response);
//        }

//        #endregion
//    }
//}
