//using FastFood.Application.Dtos;
//using FastFood.Application.Interfaces;
//using FastFood.Domain.Entities;
//using FastFood.Domain.Exceptions;
//using FastFood.Domain.Interfaces;

//namespace FastFood.Application.UseCases
//{
//    public class PermissionUseCases : IPermissionUseCases
//    {

//        private readonly IPermissionRepository _permissionRepository;

//        public PermissionUseCases(IPermissionRepository permissionRepository)
//        {
//            _permissionRepository = permissionRepository;
//        }

//        public async Task<UseCaseResult<List<Permission>>> GetAllPermissionsAsync()
//        {
//            try
//            {
//                List<Permission> permissions = new List<Permission>();

//                permissions = await _permissionRepository.GetAllPermissionsAsync();

//                if (permissions == null)
//                    throw new DomainException("Permissões não encontradas.");

//                return UseCaseResult<List<Permission>>.Success(permissions);
//            }
//            catch (DomainException ex)
//            {
//                return UseCaseResult<List<Permission>>.Failure(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return UseCaseResult<List<Permission>>.Failure("Ocorreu um erro inesperado: " + ex.Message);
//            }
//        }

//        public async Task<UseCaseResult<Permission>> GetPermissionByIdAsync(int id)
//        {
//            try
//            {

//                var permission = await _permissionRepository.GetPermissionByIdAsync(id);

//                if (permission == null)
//                    throw new DomainException("Permissão não encontrada.");


//                return UseCaseResult<Permission>.Success(permission);
//            }
//            catch (DomainException ex)
//            {
//                return UseCaseResult<Permission>.Failure(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return UseCaseResult<Permission>.Failure("Ocorreu um erro inesperado: " + ex.Message);
//            }
//        }

//        public async Task<UseCaseResult> UpdatePermissionByIdAsync(int id, EditPermissionDto permissionDto)
//        {
//            try
//            {
//                var permission = await _permissionRepository.GetPermissionByIdAsync(id);

//                if (permission == null)
//                    throw new DomainException("Permissão não encontrada.");

//                permission.UpdatePermission(permissionDto.Name);

//                await _permissionRepository.UpdatePermissionAsync(permission);

//                return UseCaseResult.Success();
//            }
//            catch (DomainException ex)
//            {
//                return UseCaseResult.Failure(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return UseCaseResult.Failure("Ocorreu um erro inesperado: " + ex.Message);
//            }
//        }

//        public async Task<UseCaseResult> AddPermissionAsync(EditPermissionDto permissionDto)
//        {
//            try
//            {
//                var permission = Permission.Create(permissionDto.Name);

//                await _permissionRepository.InsertPermissionAsync(permission);

//                return UseCaseResult.Success();
//            }
//            catch (DomainException ex)
//            {
//                return UseCaseResult.Failure(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return UseCaseResult.Failure("Ocorreu um erro inesperado: " + ex.Message);
//            }
//        }

//        public async Task<UseCaseResult> DeletePermissionByIdAsync(int id)
//        {
//            try
//            {
//                var permission = await _permissionRepository.GetPermissionByIdAsync(id);

//                if (permission == null)
//                    throw new DomainException("Permissão não encontrada.");

//                await _permissionRepository.DeletePermissionByIdAsync(permission.Id);

//                return UseCaseResult.Success();
//            }
//            catch (DomainException ex)
//            {
//                return UseCaseResult.Failure(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                return UseCaseResult.Failure("Ocorreu um erro inesperado: " + ex.Message);
//            }
//        }
//    }
//}
