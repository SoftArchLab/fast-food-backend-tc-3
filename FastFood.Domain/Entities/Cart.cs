using FastFood.Domain.Exceptions;

﻿namespace FastFood.Domain.Entities
{
    public class Cart
    {
        #region Properties

        public int Id { get; private set; }
        public Guid UserId { get; private set; }
        public decimal Subtotal { get; private set; }
        public bool IsFinished { get; private set; }
        public virtual List<CartItem> CartItems { get; private set; } = new List<CartItem>();

        #endregion

        public Cart () { }


        private Cart(Guid userId) {
            UserId = userId;
        }

        public static Cart CreateCart(Guid userId)
        {
            return new Cart(userId);
        }

        public CartItem AddOrUpdateCartItemInCart(CartItem cartItem)
        {
            if (cartItem == null)
                throw new DomainException("Por favor informe um item para o carrinho.");
            if (cartItem.Quantity <= 0)
                throw new DomainException("Quantidade do item do carrinho deve ser maior que zero.");

            var existingItem = CartItems?.FirstOrDefault(i => i.ProductId == cartItem.ProductId);

            if(existingItem != null)
            {
                existingItem.IncreaseQuantity(cartItem.Quantity);
                return existingItem;
            }
            else
            {
                CartItems?.Add(cartItem);
                return cartItem;
            }

        }

        public void RemoveCartItem(CartItem cartItem)
        {
            if (cartItem == null)
                throw new DomainException("Item do carrinho não pode ser nulo.");
            if (!CartItems.Contains(cartItem))
                throw new DomainException("Item do carrinho não encontrado.");
            CartItems.Remove(cartItem);
        }

        public void UpdateCartItem(CartItem cartItem, int quantity)
        {
            if (cartItem == null)
                throw new DomainException("Item do carrinho não pode ser nulo.");
            if (!CartItems.Contains(cartItem))
                throw new DomainException("Item do carrinho não encontrado.");
            cartItem.SetQuantity(quantity);
        }

        public decimal GetTotalPrice()
        {
            if (CartItems == null || !CartItems.Any())
                throw new DomainException("Carrinho vazio.");
            return CartItems.Sum(i => i.GetSubTotal());
        }

        public void UpdateTotalPrice()
        {
            Subtotal = GetTotalPrice();
        }

        public void ClearCart()
        {
            CartItems.Clear();
        }
        public bool ValidateIdCart(int id)
        {

            if(id <= 0)
                throw new DomainException("Id do carrinho inválido");

            return true;
        }
        public bool ValidateUserId(Guid id)
        {
            if (id <= Guid.Empty)
                throw new DomainException("Id do usuário inválido");

            return true;
        }
    }
}
