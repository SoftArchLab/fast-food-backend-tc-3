using FastFood.Domain.Entities;

namespace FastFood.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>?> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);
        Task UpdateUserAsync(User user);
        Task InsertUserAsync(User user);
        Task DeleteUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByTaxIdAsync(string taxId);
    }
}
