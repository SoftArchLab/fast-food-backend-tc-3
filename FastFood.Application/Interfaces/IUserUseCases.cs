using FastFood.Application.Dtos.Token;
using FastFood.Application.Dtos.User;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;

namespace FastFood.Application.Interfaces
{
    public interface IUserUseCases
    {
        Task<UseCaseResult> GetUserByIdAsync(Guid id);
        Task<UseCaseResult<User>> UpdateUserByIdAsync(Guid id, User user);
        Task<UseCaseResult<User>> AddUserAsync(User user);
        Task<UseCaseResult> DeleteUserByIdAsync(Guid id);
    }
}
