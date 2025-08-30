using FastFood.Application.Dtos.CartItem;
using FastFood.Application.UseCases;
using FastFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Application.Interfaces
{
    public interface ICartItemUseCases
    {
        Task<UseCaseResult<CartItem>> ValidateCartItem(CartItem cartItem);
    }
}
