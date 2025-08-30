using FastFood.Application.Dtos.Cart;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;

namespace FastFood.Application.Presenters
{
    public class CartPresenter
    {
        public ResponseCartDto ToResponseCartDto(Cart cart)
        {
            if (cart == null)
            {
                return null;
            }
            return new ResponseCartDto
            {
                Id = cart.Id,
                IsFinished = cart.IsFinished,
                Subtotal = cart.Subtotal,
                UserId = cart.UserId
            };
        }
        public UseCaseResult<ResponseCartDto> ToResponseCartDto(UseCaseResult<Cart> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<ResponseCartDto>.Failure(useCaseResult?.Message);
            }
            var responseCartDto = ToResponseCartDto(useCaseResult.Data);
            return UseCaseResult<ResponseCartDto>.Success(responseCartDto, useCaseResult.Message);
        }

        public IEnumerable<ResponseCartDto> ToResponseCartDtos(IEnumerable<Cart> Carts)
        {
            if (Carts == null || !Carts.Any())
            {
                return Enumerable.Empty<ResponseCartDto>();
            }
            return Carts.Select(ToResponseCartDto);
        }
        public UseCaseResult<IEnumerable<ResponseCartDto>> ToResponseCartDtos(UseCaseResult<IEnumerable<Cart>> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<IEnumerable<ResponseCartDto>>.Failure(useCaseResult?.Message);
            }
            var responseCartDtos = ToResponseCartDtos(useCaseResult.Data);
            return UseCaseResult<IEnumerable<ResponseCartDto>>.Success(responseCartDtos, useCaseResult.Message);
        }
    }
}
