using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gestion.Seguridad.Backend.Domain.Helpers
{
    public class JwtHelper
    {
        public string GenerateToken(Jwt jwt)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {};

            var currentTime = DateTime.Now;

            var jwtSecurityToken = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: currentTime.AddMinutes(30),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }

    public class Jwt
    {
        public string SecretKey { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
    }
}
