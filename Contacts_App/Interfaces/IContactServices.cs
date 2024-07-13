using Contacts_App.Models;

namespace Contacts_App.Interfaces
{
    public interface IContactServices
    {
        Task<bool> AddContact(Contacts contact, string token);
        Task<IEnumerable<Contacts>> GetAllContacts(string type);
        Task<bool> spamSwitch(string contactId);
    }
}
