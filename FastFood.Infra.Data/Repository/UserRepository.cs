using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly FastFoodDbContext _context;

        public UserRepository(FastFoodDbContext context)
        {
            _context = context;
        }

        public async Task DeleteUserByIdAsync(Guid id)
        {
            await _context.Users
                .Where(x => x.Id.Equals(id))
                .ExecuteDeleteAsync();
        }

        public async Task<List<User>?> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Where(x => x.Email.Equals(email))
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<User?> GetUserByTaxIdAsync(string taxId)
        {
            return await _context.Users
                .Where(x => x.TaxId.Equals(taxId)) 
                .FirstOrDefaultAsync(); 
        }

        public async Task InsertUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            await _context.Users
                .Where(x => x.Id.Equals(user.Id))
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(p => p.Name, user.Name)
                    .SetProperty(p => p.Email, user.Email)
                    .SetProperty(p => p.Password, user.Password)
                    .SetProperty(p => p.TaxId, user.TaxId)
                    .SetProperty(p => p.Role, user.Role));
        }
    }
}
