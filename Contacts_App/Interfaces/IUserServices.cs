using Contacts_App.Models;

namespace Contacts_App.Interfaces
{
    public interface IUserServices
    {
        Task<bool> Register(Users user);
        Task<string> Login(Users creds);
        Task<IEnumerable<Users>> GetAllUsers();
    }
}
