using FastFood.Domain.Enums;

namespace FastFood.Application.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaxId { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}
