using FastFood.Application.Dtos.User;
using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Repository;
using System.Data;
using System.Xml.Linq;

namespace FastFood.Gateway
{
    // Gateway fica responsavel por criar a conexão para a entidade de usuário.
    public class UserGateway
    {
        private readonly IDataSource _dataSource;
        private readonly IUserRepository _userRepository;

        public UserGateway(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _userRepository = new UserRepository(_dataSource.GetFastFoodContext()); // Instanciamos o construtor com a criação da conexão
        }

        public User ToEntity(CreateUserDto userDto, UserRole typeRole)
        {
            return User.Create(userDto.Name, 
                               userDto.TaxId, 
                               userDto.Email, 
                               userDto.Password,
                               typeRole);
        }
        public User ToEntityUpdate(Guid id, UpdateUserDto userDto)
        {
            return User.Update(id,
                               userDto.Name,
                               userDto.TaxId,
                               userDto.Email,
                               userDto.Password,
                               (UserRole)userDto.Role);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                return await _userRepository.GetAllUsersAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                return await _userRepository.GetUserByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user.", ex);
            }
        }

        public async Task<User> GetUserByTaxIdAsync(string taxId)
        {
            try
            {
                return await _userRepository.GetUserByTaxIdAsync(taxId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user.", ex);
            }
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _userRepository.GetUserByEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user.", ex);
            }
        }

        public async Task InsertUserAsync(User user)
        {
            try
            {
                await _userRepository.InsertUserAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user.", ex);
            }
        }

        public async Task UpdateUserByIdAsync(User user)
        {
            try
            {
                await _userRepository.UpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user.", ex);
            }
        }

        public async Task DeleteUserByIdAsync(Guid id)
        {
            try
            {
                await _userRepository.DeleteUserByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving user.", ex);
            }
        }
    }
}
