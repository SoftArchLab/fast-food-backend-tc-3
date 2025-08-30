using FastFood.Application.Dtos.User;
using FastFood.Application.Presenters;
using FastFood.Application.UseCases;
using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Gateway;
using FastFood.Services;

namespace FastFood.CoreController
{
    public class UserController
    {
        private readonly IDataSource _dataSource;
        private readonly UserGateway _gateway;
        private readonly UserUseCases _useCase;
        private readonly CartUseCases _cartUseCases;
        private readonly UserPresenter _presenter;

        public UserController(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _gateway = new UserGateway(_dataSource);
            _useCase = new UserUseCases();
            _cartUseCases = new CartUseCases();
            _presenter = new UserPresenter();
        }

        // Gateway: Tradução e conexão com a camada externa (camada 3 e 4).
        // UseCase: Responsável pela validação da regra de negocio (Entidade)
        // Presenter: Traduzindo o retorno para a resposta esperada na WebController

        public async Task<IEnumerable<ResponseUserDto>> GetAllUsers()
        {
            var response = await _gateway.GetUsers();

            return _presenter.ToResponseUserDtos(response);
        }

        public async Task<User> GetUserById(Guid id)
        {
            var usecase = await _useCase.GetUserByIdAsync(id);
            return await _gateway.GetUserById(id);
        }

        public async Task<UseCaseResult<ResponseUserDto>> AddUser(CreateUserDto userDto, UserRole typeRole)
        {
            var user = _gateway.ToEntity(userDto, typeRole);
            var useCase = await _useCase.AddUserAsync(user);

            if (useCase.IsSuccess)
            {
                await _gateway.InsertUserAsync(user);
            }

            return _presenter.ToResponseUserDto(useCase);
        }

        public async Task<UseCaseResult<ResponseUserDto>> EditUser(Guid id, UpdateUserDto userDto)
        {
            var user = _gateway.ToEntityUpdate(id, userDto);
            var useCase = await _useCase.UpdateUserByIdAsync(id, user);

            if (useCase.IsSuccess)
            {
                await _gateway.UpdateUserByIdAsync(user);
            }

            return _presenter.ToResponseUserDto(useCase);
        }

        public async Task<UseCaseResult> DeleteUser(Guid id)
        {
            var useCase = await _useCase.DeleteUserByIdAsync(id);

            if (useCase.IsSuccess)
            {
                await _gateway.DeleteUserByIdAsync(id);
            }

            return useCase;
        }

        public async Task<UseCaseResult<ResponseUserAuthDto>> AuthenticateAsync(AuthenticateUserDto authDto)
        {
            var isValid = _useCase.ValidateUserToAuth(authDto);

            if (!isValid.IsSuccess)
            {
                return UseCaseResult<ResponseUserAuthDto>.Failure(isValid.Message ?? "Dados inválidos");
            }

            UserDto userDto;

            if (authDto.IsGuest)
            {
                var guestUser = _useCase.CreateGuestUser(authDto).Data;
                await _gateway.InsertUserAsync(guestUser);
                userDto = _useCase.GetUserToAuth(guestUser).Data;
            }
            else
            {
                var user = !string.IsNullOrEmpty(authDto.TaxId)
                        ? await _gateway.GetUserByTaxIdAsync(authDto.TaxId)
                        : await _gateway.GetUserByEmailAsync(authDto.Email);
                userDto = _useCase.GetUserToAuth(user).Data;
            }

            var token = TokenHelper.GenerateToken(userDto);

            return _presenter.ToResponseUserAuthDto(userDto, token);
        }
    }
}
