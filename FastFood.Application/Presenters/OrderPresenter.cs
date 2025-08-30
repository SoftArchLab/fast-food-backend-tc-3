using FastFood.Application.Dtos.Order;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Presenters
{
    public class OrderPresenter
    {
        public ResponseOrderDto ToResponseOrderDto(Order order)
        {
            if (order == null)
            {
                return null;
            }
            return new ResponseOrderDto
            {
                Id = order.Id,
                CartId = order.CartId,
                CompletionDate = order.CompletionDate,
                CreatedDate = order.CreatedDate,
                OrderStatusId = order.OrderStatusId,
                PaymentId = order.PaymentId,
                Total = order.Total,
                UserId = order.UserId
            };
        }
        public UseCaseResult<ResponseOrderDto> ToResponseOrderDto(UseCaseResult<Order> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<ResponseOrderDto>.Failure(useCaseResult?.Message);
            }
            var responseOrderDto = ToResponseOrderDto(useCaseResult.Data);
            return UseCaseResult<ResponseOrderDto>.Success(responseOrderDto, useCaseResult.Message);
        }

        public IEnumerable<ResponseOrderDto> ToResponseOrderDtos(IEnumerable<Order> orders)
        {
            if (orders == null || !orders.Any())
            {
                return Enumerable.Empty<ResponseOrderDto>();
            }
            return orders.Select(ToResponseOrderDto);
        }
        public UseCaseResult<IEnumerable<ResponseOrderDto>> ToResponseOrderDtos(UseCaseResult<IEnumerable<Order>> useCaseResult)
        {
            if (useCaseResult == null || !useCaseResult.IsSuccess)
            {
                return UseCaseResult<IEnumerable<ResponseOrderDto>>.Failure(useCaseResult?.Message);
            }
            var responseOrderDtos = ToResponseOrderDtos(useCaseResult.Data);
            return UseCaseResult<IEnumerable<ResponseOrderDto>>.Success(responseOrderDtos, useCaseResult.Message);
        }
    }
}
