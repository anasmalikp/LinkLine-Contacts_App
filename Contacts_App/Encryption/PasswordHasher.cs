using Contacts_App.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Contacts_App.Encryption
{
    public class PasswordHasher:IPasswordHasher
    {
        public string HashPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public bool VerifyPassword(string password, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashed);
        }

        public string TokenDecoder(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var decoded = tokenHandler.ReadJwtToken(token);
            var userId = decoded.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            return userId;
        }
    }
}
