using FastFood.Application.Dtos.Order;
using FastFood.Application.Interfaces;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Exceptions;
using FastFood.Domain.Interfaces;
using Newtonsoft.Json;

namespace FastFood.Application.UseCases
{
    public class OrderUseCases : IOrderUseCases
    {
        public OrderUseCases()
        {
        }

        public async Task<UseCaseResult> ValidateOrderId(int id)
        {
            try
            {
                if (id == 0)
                    throw new DomainException("Id da Ordem inválido.");

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

        public async Task<UseCaseResult> ValidateOrderStatus(string status)
        {
            try
            {
                if(status == string.Empty)
                    throw new DomainException("Status não encontrado.");

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

    }
}
