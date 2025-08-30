using FastFood.Application.Dtos.User;

namespace FastFood.Application.Dtos.Token
{
    public class TokenDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
