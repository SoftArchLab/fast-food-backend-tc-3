using FastFood.Application.Dtos.Cart;
using FastFood.Application.Dtos.CartItem;
using FastFood.Application.Presenters;
using FastFood.Application.UseCases;
using FastFood.DataSource;
using FastFood.Domain.Entities;
using FastFood.Gateway;

namespace FastFood.CoreController
{
    public class CartController
    {
        private readonly IDataSource _dataSource;
        private readonly CartGateway _gatewayCart;
        private readonly CartItemGateway _gatewayCartItem;
        private readonly CartUseCases _useCaseCart;
        private readonly CartItemUseCases _useCaseCartItem;
        private readonly CartPresenter _presenterCart;
        private readonly CartItemPresenter _presenterCartItem;

        public CartController(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _gatewayCart = new CartGateway(_dataSource);
            _gatewayCartItem = new CartItemGateway(_dataSource);
            _useCaseCart = new CartUseCases();
            _useCaseCartItem = new CartItemUseCases();
            _presenterCart = new CartPresenter();
            _presenterCartItem = new CartItemPresenter();
        }

        public async Task<ResponseCartDto> GetUserCart(Guid userId)
        {
            var useCase = await _useCaseCart.ValidateUserId(userId);
            var response = await _gatewayCart.GetUserCartAsync(userId);

            return _presenterCart.ToResponseCartDto(response);
        }

        public async Task<ResponseCartDto> AddCartItem(CartItemDto cartItemDto)
        {
            var gtwCartItem = _gatewayCartItem.ToEntity(cartItemDto); // Traduz as informações de entrada (DTO <> Entidade)
            var ucCartItem = await _useCaseCartItem.ValidateCartItem(gtwCartItem); // Valida a entidade convertida (RN)
            var ucUserId = await _useCaseCart.ValidateUserId(cartItemDto.UserId); // Validamos o ID usuário
            var gtwCart = await _gatewayCart.GetUserCartAsync(cartItemDto.UserId); // Busca o carrinho usando o ID do usuário

            // Entra se a validação der certo
            if (ucCartItem.IsSuccess)
            {
                if (gtwCart == null)
                {  // Se o carrinho for nulo, criamos um do zero
                    gtwCart = _gatewayCart.CreateCartByUserId(cartItemDto.UserId);  // Criamos o carrinho e retornamos
                    await _gatewayCart.CreateCartAsync(gtwCart);
                }

                var lastCartItemsCount = gtwCart.CartItems.Count(); // Seta a quantidade de itens que o carrinho antigo tem

                var cartItem = _useCaseCart.AddOrUpdateCartItemInCart(gtwCart, gtwCartItem); // Insere/Atualiza o item já validado, no carrinho e retornamos

                var nowCartItemsCount = gtwCart.CartItems.Count(); // Seta a quantidade de itens que o carrinho atual tem

                if (nowCartItemsCount != lastCartItemsCount) // Compara a diferença de itens no carinho ATUAL X ANTIGO
                    await _gatewayCartItem.AddCartItemAsync(cartItem); // Salva o novo item do carrinho
                else
                    await _gatewayCartItem.UpdateCartItemAsync(cartItem); // Salva alteração na quantidade do item

                gtwCart = await _gatewayCart.GetUserCartAsync(cartItemDto.UserId);

                // Atualiza o subTotal do carrinho
                gtwCart.UpdateTotalPrice(); // Atualiza o preço do carrinho
                await _gatewayCart.UpdateCartAsync(gtwCart); // Atualiza o carrinho na base
            }

            return _presenterCart.ToResponseCartDto(gtwCart);
        }

        public async Task<ResponseCartDto> RemoveCartItem(CartItemDto cartItemDto)
        {
            var gtwCartItem = _gatewayCartItem.ToEntity(cartItemDto); // Traduz as informações de entrada (DTO <> Entidade)
            var ucCartItem = await _useCaseCartItem.ValidateCartItem(gtwCartItem); // Valida a entidade convertida (RN)
            var ucUserId = await _useCaseCart.ValidateUserId(cartItemDto.UserId); // Validamos o ID usuário
            var gtwCart = await _gatewayCart.GetUserCartAsync(cartItemDto.UserId); // Busca o carrinho usando o ID do usuário

            if (ucCartItem.IsSuccess)
            {
                var cartItem = gtwCart.CartItems.FirstOrDefault(f => f.ProductId == cartItemDto.ProductId); // Pega o CartItem que deseja remover

                if (cartItem != null)
                {
                    if (cartItemDto.Quantity < cartItem.Quantity)
                    {
                        cartItem.DecreaseQuantity(cartItemDto.Quantity);
                        await _gatewayCartItem.UpdateCartItemAsync(cartItem); // Atualiza a quantidade do item
                    }
                    else
                    {
                        await _gatewayCartItem.RemoveCartItemAsync(cartItem.Id); // Remove o item no carrinho
                        gtwCart = await _gatewayCart.GetUserCartAsync(cartItemDto.UserId); // Remove o item em memória, ao invés de ir na base e buscar novamente
                    }

                    gtwCart.UpdateTotalPrice(); // Atualiza o preço do carrinho
                    gtwCart = await _gatewayCart.UpdateCartAsync(gtwCart); // Atualiza o carrinho na base
                }
            }

            return _presenterCart.ToResponseCartDto(gtwCart);
        }
    }
}
