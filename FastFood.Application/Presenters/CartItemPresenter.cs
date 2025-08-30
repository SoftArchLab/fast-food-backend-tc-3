using FastFood.Application.Dtos.Cart;
using FastFood.Application.Dtos.CartItem;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Presenters
{
    public class CartItemPresenter
    {
        public ResponseCartItemDto ToResponseCartItemDto(CartItem cartItem)
        {
            if (cartItem == null)
            {
                return null;
            }
            return new ResponseCartItemDto
            {
                Id = cartItem.Id,
                CartId = cartItem.CartId,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };
        }
        public UseCaseResult<ResponseCartItemDto> ToResponseCartItemDto(UseCaseResult<CartItem> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<ResponseCartItemDto>.Failure(useCaseResult?.Message);
            }
            var responseCartItemDto = ToResponseCartItemDto(useCaseResult.Data);
            return UseCaseResult<ResponseCartItemDto>.Success(responseCartItemDto, useCaseResult.Message);
        }

        public IEnumerable<ResponseCartItemDto> ToResponseCartItemDtos(IEnumerable<CartItem> cartItems)
        {
            if (cartItems == null || !cartItems.Any())
            {
                return Enumerable.Empty<ResponseCartItemDto>();
            }
            return cartItems.Select(ToResponseCartItemDto);
        }
        public UseCaseResult<IEnumerable<ResponseCartItemDto>> ToResponseCartItemDtos(UseCaseResult<IEnumerable<CartItem>> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<IEnumerable<ResponseCartItemDto>>.Failure(useCaseResult?.Message);
            }
            var responseCartItemDtos = ToResponseCartItemDtos(useCaseResult.Data);
            return UseCaseResult<IEnumerable<ResponseCartItemDto>>.Success(responseCartItemDtos, useCaseResult.Message);
        }
    }
}
