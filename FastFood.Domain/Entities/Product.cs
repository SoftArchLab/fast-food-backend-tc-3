using FastFood.Domain.Exceptions;

namespace FastFood.Domain.Entities
{
    public class Product
    {
        #region Properties

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }
        public int CategoryId { get; private set; }

        public virtual Category Category { get; private set; }

        #endregion

        #region Constuctor
        
        public Product() { }
        private Product(string name, string description, decimal price, int initialStock, int categotyId)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = initialStock;
            CategoryId = categotyId;
        }
        private Product(int id, string name, string description, decimal price, int initialStock, int categotyId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = initialStock;
            CategoryId = categotyId;
        }

        #endregion

        #region Methods
        public static Product Create(string name, string description, decimal price, int initialStock, int categotyId)
        {
            var product = new Product(
                name,
                description,
                price,
                initialStock,
                categotyId
            );

            return product;
        }
        public static Product Update(int id, string name, string description, decimal price, int stockQuantity, int categoryId)
        {
            var product = new Product(
                id,
                name,
                description,
                price,
                stockQuantity,
                categoryId
            );

            return product;
        }
        public void UpdatePrice(decimal price)
        {
            ValidatePrice(price);
            Price = price;
        }
        public void DesactivateProduct()
        {
            IsActive = false;
        }
        public void ActivateProduct()
        {
            IsActive = true;
        }
        public void IncreaseStock(int quantity)
        {
            ValidateStock(quantity);
            StockQuantity += quantity;
        }
        public void DecreaseStock(int quantity)
        {
            ValidateStock(quantity);

            if (quantity > StockQuantity)
                throw new DomainException("Quantidade insuficiente em estoque");

            StockQuantity -= quantity;
        }
        public void UpdateCategory(int categoryId)
        {
            CategoryId = categoryId;
        }
        #endregion

        #region Validations
        public bool ValidateName(string name)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(name)) 
                result = true;

            return result;
        }
        public bool ValidateDescription(string description)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(description))
                result = true;

            return result;
        }
        public bool ValidatePrice(decimal price)
        {
            bool result = false;

            if (price >= 0)
                result = true;

            return result;
        }
        public bool ValidateStock(int stock)
        {
            bool result = false;

            if (stock > 0)
                result = true;

            return result;
        }
        #endregion
    }
}
