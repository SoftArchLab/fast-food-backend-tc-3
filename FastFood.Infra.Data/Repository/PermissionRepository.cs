//using FastFood.Domain.Entities;
//using FastFood.Domain.Interfaces;
//using FastFood.Infra.Data.Context;
//using Microsoft.EntityFrameworkCore;

//namespace FastFood.Infra.Data.Repository
//{
//    public class PermissionRepository : IPermissionRepository
//    {

//        private readonly FastFoodDbContext _context;

//        public PermissionRepository(FastFoodDbContext context)
//        {
//            _context = context;
//        }

//        public async Task DeletePermissionByIdAsync(int id)
//        {
//            await _context.Permissions
//                .Where(x => x.Id.Equals(id))
//                .ExecuteDeleteAsync();
//        }

//        public async Task<List<Permission>?> GetAllPermissionsAsync()
//        {
//            return await _context.Permissions.ToListAsync();
//        }

//        public async Task<Permission?> GetPermissionByIdAsync(int id)
//        {
//            return await _context.Permissions.FirstOrDefaultAsync(x => x.Id.Equals(id));
//        }

//        public async Task InsertPermissionAsync(Permission permission)
//        {
//            await _context.Permissions.AddAsync(permission);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdatePermissionAsync(Permission permission)
//        {
//            await _context.Permissions
//                .Where(x => x.Id.Equals(permission.Id))
//                .ExecuteUpdateAsync(x =>
//                    x.SetProperty(p => p.Name, permission.Name));
//        }
//    }
//}
