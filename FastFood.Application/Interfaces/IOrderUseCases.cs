using FastFood.Application.Dtos.Order;
using FastFood.Application.UseCases;

namespace FastFood.Application.Interfaces
{
    public interface IOrderUseCases
    {
        Task<UseCaseResult> ValidateOrderId(int id);
        Task<UseCaseResult> ValidateOrderStatus(string status);
    }
}
