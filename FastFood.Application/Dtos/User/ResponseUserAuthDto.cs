using FastFood.Domain.Enums;

namespace FastFood.Application.Dtos.User
{
    public class ResponseUserAuthDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }

        public ResponseUserAuthDto(UserDto user, string token)
        {
            User = user;
            Token = token;
        }
    }
}