using Contacts_App.ContextFolder;
using Contacts_App.Interfaces;
using Contacts_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Contacts_App.Services
{
    public class UserServices:IUserServices
    {
        private readonly ContextClass context;
        private readonly ILogger<UserServices> logger;
        private readonly IPasswordHasher hasher;
        private readonly IConfiguration config;
        public UserServices(ContextClass context, IPasswordHasher hasher, IConfiguration config)
        {
            this.context = context;
            this.hasher = hasher;
            this.config = config;
        }

        public async Task<bool> Register(Users user)
        {
            try
            {
                var existingUser = await context.usersTable.FirstOrDefaultAsync(x => x.email == user.email);
                if(existingUser != null)
                {
                    logger.LogError("user already exists");
                    return false;
                }
                var hashed = hasher.HashPassword(user.password);
                user.id = Guid.NewGuid().ToString();
                user.password = hashed;
                await context.usersTable.AddAsync(user);
                await context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            try
            {
                return await context.usersTable.ToListAsync();
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<string> Login(Users creds)
        {
            try
            {
                if (creds != null)
                {
                    var user = await context.usersTable.FirstOrDefaultAsync(x => x.email == creds.email);
                    if (user == null)
                    {
                        logger.LogInformation("User Doesn't exists");
                        return null;
                    }
                    var isVerified = hasher.VerifyPassword(creds.password, user.password);
                    if(isVerified)
                    {
                        return GetToken(user);
                    }
                    logger.LogError("wrong password");
                    return null;
                }
                logger.LogError("invalid input");
                return null;
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        private string GetToken(Users user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["jwt:key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.id),
                };
                var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddDays(1)
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
