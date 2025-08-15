using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TaskScheduler.API.Domain.Repositories
{
    public interface IJwtRepository
    {
        string GetJwtToken(string email);
    }

    public class JwtRepository : IJwtRepository
    {
        private readonly string jwtKey;
        private readonly string jwtIssuer;
        private readonly string jwtAudience;

        private IConfiguration _configuration;
        public JwtRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            (jwtKey, jwtIssuer, jwtAudience) = (
                _configuration["JwtSettings:Key"]
                    ?? throw new Exception("Jwt key is missing in AppSettings"),
                _configuration["JwtSettings:Issuer"]
                    ?? throw new Exception("Jwt issuer is missing in AppSettings"),
                _configuration["JwtSettings:Audience"]
                    ?? throw new Exception("Jwt audience is missing in AppSettings")
            );
        }

        public string GetJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ExtractEmail(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, 
                ValidateAudience = true,
                ValidateLifetime = true, 
                ClockSkew = TimeSpan.Zero, 
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true
            };

            try
            {
               
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                var emailClaim = principal.Claims
                    .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);

                return emailClaim?.Value ?? throw new Exception("Email claim not found in token");
            }
            catch (SecurityTokenExpiredException)
            {
                throw new Exception("Token has expired");
            }
            catch (SecurityTokenException ex)
            {
                throw new Exception($"Invalid token: {ex.Message}");
            }
        }
    }
}