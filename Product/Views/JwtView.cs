using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Product.Views
{
    public class JwtView
    {
        private static string SecretKey = "";
        private static string Issuer = "";
        private static string Audience = "";

        public static void GetConfiguration(IConfiguration configuration)
        {
            SecretKey = configuration["Jwt:SecretKey"];
            Issuer = configuration["Jwt:Issuer"];
            Audience = configuration["Jwt:Audience"];
        }

        public static string GenerateJwtToken(IEnumerable<Claim> claim)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claim),
                Issuer = Issuer,
                Audience = Audience,
                Expires = DateTime.UtcNow.AddDays(1),
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken tokenObj = tokenHandler.CreateToken(tokenDescriptor);

            string tokenString = tokenHandler.WriteToken(tokenObj);
            return tokenString;
        }
    }
}
