using FastFood.Application.Dtos.Order;
using FastFood.Application.Presenters;
using FastFood.Application.UseCases;
using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Gateway;

namespace FastFood.CoreController
{
    public class OrderController
    {
        private readonly IDataSource _dataSource;
        private readonly OrderGateway _gateway;
        private readonly CartGateway _gatewayCart;
        private readonly OrderUseCases _useCase;
        private readonly CartUseCases _useCaseCart;
        private readonly OrderPresenter _presenter;

        public OrderController(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _gateway = new OrderGateway(_dataSource);
            _gatewayCart = new CartGateway(_dataSource);
            _useCase = new OrderUseCases();
            _useCaseCart = new CartUseCases();
            _presenter = new OrderPresenter();
        }

        public async Task<IEnumerable<ResponseOrderDto>> GetAllUsers()
        {
            var response = await _gateway.GetOrders();

            return _presenter.ToResponseOrderDtos(response);
        }

        public async Task<ResponseOrderDto> GetOrderById(int id)
        {
            var response = await _useCase.ValidateOrderId(id);
            var order = await _gateway.GetOrderById(id);

            return _presenter.ToResponseOrderDto(order);
        }

        public async Task<IEnumerable<ResponseOrderDto>> GetOrdersFromOrderStatus(string status)
        {
            var response = await _useCase.ValidateOrderStatus(status);

            if (!response.IsSuccess)
                throw new Exception("Status da ordem inválido.");

            var orders = await _gateway.GetOrdersFromOrderStatus(status);

            return _presenter.ToResponseOrderDtos(orders);
        }
        
        public async Task<ResponseOrderDto> UpdateOrderStatusById(int id)
        {
            var response = await _useCase.ValidateOrderId(id);

            if (!response.IsSuccess)
                throw new Exception("Id da Ordem inválido.");

            var order = await _gateway.GetOrderById(id);

            order.UpdateStatus();

            await _gateway.UpdateOrderStatusByIdAsync(order);
            
            return _presenter.ToResponseOrderDto(order);
        }

        public async Task<UseCaseResult> GenerateOrderFromCart(int cartId)
        {
            var response = await _useCaseCart.ValidateCartId(cartId);

            var orderByCart = _gateway.GetOrderByCartId(cartId);

            // Caso não exista uma ordem para o ID do carrinho.
            // Cria uma nova
            if (orderByCart.Result == null || orderByCart.Result.Id == 0)
            {
                var cart = await _gatewayCart.GetCartAsync(cartId);
                var order = _gateway.CreateOrder(cart);
                await _gateway.InsertOrderAsync(order);

            }

            return response;
        }
    }
}
