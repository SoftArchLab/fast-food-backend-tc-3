using FastFood.Domain.Exceptions;

namespace FastFood.Domain.Entities
{
    public class CartItem
    {
        #region Properties

        public int Id { get; private set; }
        public int Quantity { get; private set; }
        public int ProductId { get; private set; }
        public virtual Product Product { get; private set; }
        public int CartId { get; private set; }
        public virtual Cart Cart { get; private set; } 

        #endregion

        public CartItem()
        {

        }

        private CartItem(int quantity, int productId, int cartId)
        {
            Quantity = quantity;
            ProductId = productId;
            CartId = cartId;
        }

        public static CartItem Create(int quantity, int productId, int cartId)
        {
            ValidateQuantity(quantity);
            ValidateProductId(productId);

            if (cartId <= 0) 
                throw new DomainException("CartId inválido.");

            return new CartItem(quantity, productId, cartId);
        }

        public void IncreaseQuantity(int quantity)
        {
            ValidateQuantity(quantity);
            Quantity += quantity;
        }

        public void DecreaseQuantity(int quantity)
        {
            ValidateQuantity(quantity);
            if (quantity > Quantity)
                throw new DomainException("Quantidade insuficiente no carrinho.");
            Quantity -= quantity;
        }

        public void SetQuantity(int quantity)
        {
            ValidateQuantity(quantity);
            if (quantity > 0)
                Quantity = quantity;
        }

        public decimal GetSubTotal()
        {
            if (Product == null)
                throw new DomainException("Produto não pode ser nulo.");
            return Product.Price * Quantity;
        }

        private static void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantidade do item do carrinho deve ser maior que zero.");
        }

        private static void ValidateProductId(int productId)
        {
            if (productId <= 0)
                throw new DomainException("ProductId inválido.");
        }
    }
}