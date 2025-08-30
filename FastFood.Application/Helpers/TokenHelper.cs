using FastFood.Application.Dtos.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FastFood.Services
{
    public static class TokenHelper
    {
        public static string GenerateToken(UserDto user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),

                Subject = new ClaimsIdentity(new[]
                {
                           new Claim("id", user.Id.ToString()),
                           new Claim("taxId", user.TaxId ?? string.Empty),
                           new Claim("email", user.Email ?? string.Empty),
                           new Claim(ClaimTypes.Role, user.Role.ToString())
                    }),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
