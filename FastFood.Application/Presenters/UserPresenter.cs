using FastFood.Application.Dtos.User;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;

namespace FastFood.Application.Presenters
{
    public class UserPresenter
    {
        public ResponseUserDto ToResponseUserDto(User user)
        {
            if (user == null)
            {
                return null;
            }
            return new ResponseUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                TaxId = user.TaxId,
                Role = user.Role,
            };
        }

        public UseCaseResult<ResponseUserAuthDto> ToResponseUserAuthDto(UserDto userDto, string token)
        {
            var response = new ResponseUserAuthDto(userDto, token);

            if (response.User.Role == UserRole.Guest)
                return UseCaseResult<ResponseUserAuthDto>.Success(response, "Usuário autenticado como convidado");
            else if (response.User.Role == UserRole.Customer)
                return UseCaseResult<ResponseUserAuthDto>.Success(response, $"Bem vindo {response.User.Name}");

            return UseCaseResult<ResponseUserAuthDto>.Success(response, $"Bem vindo {response.User.Name}");
        }

        public UseCaseResult<ResponseUserDto> ToResponseUserDto(UseCaseResult<User> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<ResponseUserDto>.Failure(useCaseResult?.Message);
            }
            var responseUserDto = ToResponseUserDto(useCaseResult.Data);
            return UseCaseResult<ResponseUserDto>.Success(responseUserDto, useCaseResult.Message);
        }

        public IEnumerable<ResponseUserDto> ToResponseUserDtos(IEnumerable<User> users)
        {
            if (users == null || !users.Any())
            {
                return Enumerable.Empty<ResponseUserDto>();
            }
            return users.Select(ToResponseUserDto);
        }
        public UseCaseResult<IEnumerable<ResponseUserDto>> ToResponseUserDtos(UseCaseResult<IEnumerable<User>> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<IEnumerable<ResponseUserDto>>.Failure(useCaseResult?.Message);
            }
            var responseUserDtos = ToResponseUserDtos(useCaseResult.Data);
            return UseCaseResult<IEnumerable<ResponseUserDto>>.Success(responseUserDtos, useCaseResult.Message);
        }
    }
}
