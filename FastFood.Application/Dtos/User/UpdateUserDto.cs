using FastFood.Domain.Enums;

namespace FastFood.Application.Dtos.User
{
    public class UpdateUserDto
    {
        public string? Name { get; set; }
        public string? TaxId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole? Role { get; set; }
    }
}
