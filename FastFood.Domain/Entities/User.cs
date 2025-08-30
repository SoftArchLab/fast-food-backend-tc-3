using FastFood.Domain.Enums;
using FastFood.Domain.Exceptions;
using System.Data;

namespace FastFood.Domain.Entities
{
    public class User
    {
        #region Properties

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? TaxId { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public  UserRole Role{ get; private set; }

        #endregion

        public User()
        {
            
        }

        public User(string name, string taxId, string email, string password, UserRole role)
        {
            Name = name;
            TaxId = taxId;
            Email = email;
            Password = password;
            Role = role;
        }

        #region Methods
        public static User Create(string name, string taxId, string email,string password, UserRole role)
        {
            return new User(name, taxId, email, password, role);
        }

        public static User Update(Guid id, string name, string taxId, string email, string password, UserRole role)
        {
            return new User
            {
                Id = id,
                Name = name,
                TaxId = taxId,
                Email = email,
                Password = password,
                Role = role
            };
        }

        public void UpdateRole(UserRole role)
        {
            if (role == UserRole.Guest)
                throw new DomainException("Não é possível atualizar o usuário para Guest.");
            
            Role = role;
        }
        #endregion

        #region Validations
        public bool ValidateId(Guid id)
        {
            bool result = false;

            if (id != Guid.Empty)
                result = true;

            return result;
        }
        public bool ValidateUserName(string name)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(name) || !string.IsNullOrWhiteSpace(name))
                result = true;

            return result;
        }

        public bool ValidateTaxId(string taxId)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(taxId) || !string.IsNullOrWhiteSpace(taxId))
                result = true;

            return result;
        }

        public bool ValidateEmail(string email)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(email) || !string.IsNullOrWhiteSpace(email))
                result = true;

            return result;
        }

        public void AdminAutheticate(string? password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
                throw new DomainException("Senha inválida");
            else if (Password == null || Password != password)
                throw new DomainException("Senha incorreta");
            else if (Role != UserRole.Admin)
                throw new DomainException("Usuário não tem permissão de administrador");
        }
        #endregion

    }

}
