namespace FastFood.Domain.Entities
{
    public class Category
    {
        #region Properties

        public int Id { get; private set; }
        public string Name { get; private set; }

        public virtual ICollection<Product> Products { get; set; }

        #endregion

        #region Ctor

        public Category() { }

        public Category(string name)
        {
            Name = name;
        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }


        #endregion

        #region Methods

        #region Add

        public void AddCategory(string name)
        {
            if (!ValidadeSaveCategory(name))
                throw new ArgumentException("Categoria inválida");
            Name = name;
        }

        #endregion

        #region Edit

        public void UpdateCategory(int id, string name) 
        {
            if (!ValidadeSaveCategory(name))
                throw new ArgumentException("Categoria inválida");
            Name = name;
            Id = id;
        }

        #endregion

        #region Delete

        public void DeleteCategoryById(Category category) 
        {
            if (!IsValid(category))
                throw new ArgumentException("Categoria inválida");
        }

        #endregion

        #endregion

        #region Validations

        private bool IsValid(Category category)
        {
            if (category.Id <= 0)
                return false;
            if (string.IsNullOrEmpty(category.Name) || string.IsNullOrWhiteSpace(category.Name))
                return false;

            return true;
        }

        public bool ValidadeSaveCategory(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                return false;

            return true;
        }

        #endregion
    }
}
