using FastFood.Application.Dtos.Token;
using FastFood.Application.Dtos.User;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Exceptions;

namespace FastFood.Application.UseCases
{
    public class UserUseCases : IUserUseCases
    {

        public UserUseCases()
        {
        }

        #region Methods

        public async Task<UseCaseResult> GetUserByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new DomainException("Usuário não encontrado.");

                return UseCaseResult.Success();
            }
            catch (DomainException ex)
            {
                return UseCaseResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult.Failure("Ocorreu um erro inesperado: " + ex.Message);
            }
        }

        public async Task<UseCaseResult<User>> UpdateUserByIdAsync(Guid id, User user)
        {
            try
            {
                if (id == Guid.Empty)
                    return UseCaseResult<User>.Failure("Id od usuário inválido");

                if (user == null)
                    return UseCaseResult<User>.Failure("Usuário não pode ser nulo");

                if (!user.ValidateEmail(user.Email))
                    return UseCaseResult<User>.Failure("E-mail inválido");

                if (!user.ValidateUserName(user.Name))
                    return UseCaseResult<User>.Failure("CPF/CNPJ inválido");

                if (!user.ValidateTaxId(user.TaxId))
                    return UseCaseResult<User>.Failure("TaxId inválido");

                if (!Enum.IsDefined(typeof(UserRole), user.Role) || user.Role != UserRole.Admin && user.Role != UserRole.Guest && user.Role != UserRole.Customer)
                    return UseCaseResult<User>.Failure("Role inválida");

                return UseCaseResult<User>.Success(user);
            }
            catch (DomainException ex)
            {
                return UseCaseResult<User>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult<User>.Failure("Ocorreu um erro inesperado: " + ex.Message);
            }
        }

        public async Task<UseCaseResult<User>> AddUserAsync(User user)
        {
            try
            {
                if (user == null)
                    return UseCaseResult<User>.Failure("Usuário não pode ser nulo");

                if (!user.ValidateEmail(user.Email))
                    return UseCaseResult<User>.Failure("E-mail inválido");

                if (!user.ValidateUserName(user.Name))
                    return UseCaseResult<User>.Failure("Nome inválido");

                if (!user.ValidateTaxId(user.TaxId))
                    return UseCaseResult<User>.Failure("CPF/CNPJ inválido");

                return UseCaseResult<User>.Success(user);
            }
            catch (DomainException ex)
            {
                return UseCaseResult<User>.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult<User>.Failure("Ocorreu um erro inesperado: " + ex.Message);
            }
        }

        public async Task<UseCaseResult> DeleteUserByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new DomainException("Usuário não encontrado.");

                return UseCaseResult.Success();
            }
            catch (DomainException ex)
            {
                return UseCaseResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                return UseCaseResult.Failure("Ocorreu um erro inesperado: " + ex.Message);
            }
        }

        public async Task<UseCaseResult<TokenDto>> AutheticateAsync(AuthenticateUserDto authDto)
        {
            //try
            //{
            //    UserDto userDto;

            //    if (!authDto.IsGuest)
            //    {
            //        var user = !string.IsNullOrEmpty(authDto.TaxId)
            //            ? await _userRepository.GetUserByTaxIdAsync(authDto.TaxId)
            //            : await _userRepository.GetUserByEmailAsync(authDto.Email ?? "");

            //        if (user == null)
            //            throw new DomainException("Usuário não encontrado.");

            //        if (authDto.Password != user.Password)
            //            throw new DomainException("Senha incorreta.");

            //        userDto = this.CreateUserDtoToAuth(
            //            user.Id,
            //            user.Name,
            //            user.Email,
            //            user.TaxId,
            //            user.Role
            //        );
            //    }
            //    else
            //    {
            //        userDto = this.CreateUserDtoToAuth(
            //            Guid.NewGuid(),
            //            authDto.Name,
            //            null,
            //            null,
            //            UserRole.Guest
            //        );

            //        // Cria um usuário convidado temporário
            //        await _userRepository.InsertUserAsync(User.Create(
            //            userDto.Name,
            //            userDto.TaxId,
            //            userDto.Email,
            //            null, // Senha não é necessária para convidados
            //            userDto.Role
            //        ));
            //    }

            //    var token = TokenHelper.GenerateToken(userDto);

            //    return UseCaseResult<TokenDto>.Success(new TokenDto()
            //    {
            //        Token = token,
            //        User = userDto
            //    });
            //}
            //catch (DomainException ex)
            //{
            //    return UseCaseResult<TokenDto>.Failure(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    return UseCaseResult<TokenDto>.Failure("Ocorreu um erro inesperado: " + ex.Message);
            //}
            return UseCaseResult<TokenDto>.Failure("Método não implementado.");
        }

        public UseCaseResult<UserDto> GetUserToAuth(User user)
        {
            return UseCaseResult<UserDto>.Success(
                this.CreateUserDtoToAuth(
                    user.Id != Guid.Empty ? user.Id : Guid.NewGuid(),
                    user.Name,
                    user.Email,
                    user.TaxId,
                    user.Role
                )
            );
        }
        public UseCaseResult<User> CreateGuestUser(AuthenticateUserDto authDto)
        {
            return UseCaseResult<User>.Success(User.Create(
                    authDto.Name,
                    authDto.TaxId,
                    authDto.Email,
                    string.Empty,
                    UserRole.Guest
                ));
        }

        public UseCaseResult<AuthenticateUserDto> ValidateUserToAuth(AuthenticateUserDto authDto)
        {
            if (authDto == null)
                UseCaseResult<AuthenticateUserDto>.Failure("Dados de autenticação não podem ser nulos.");
            else if (!authDto.IsGuest)
            {
                if (string.IsNullOrWhiteSpace(authDto.TaxId))
                    return UseCaseResult<AuthenticateUserDto>.Failure("CPF/CNPJ é obrigatório.");
                else if (string.IsNullOrWhiteSpace(authDto.Email))
                    return UseCaseResult<AuthenticateUserDto>.Failure("E-mail é obrigatório.");
                else if (string.IsNullOrWhiteSpace(authDto.Password))
                    return UseCaseResult<AuthenticateUserDto>.Failure("Senha é obrigatória.");
            }
            else
            {
                if (authDto.IsGuest && string.IsNullOrEmpty(authDto.Name))
                    return UseCaseResult<AuthenticateUserDto>.Failure("Nome é obrigatório para usuários convidados.");
            }

            return UseCaseResult<AuthenticateUserDto>.Success(authDto);
        }


        private UserDto CreateUserDtoToAuth(Guid id, string name, string email, string taxId, UserRole role)
        {
            return new UserDto
            {
                Id = id,
                Name = name,
                Email = email,
                TaxId = taxId,
                Role = role,
            };
        }

        #endregion
    }
}
