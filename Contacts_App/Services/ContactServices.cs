using Contacts_App.ContextFolder;
using Contacts_App.Interfaces;
using Contacts_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts_App.Services
{
    public class ContactServices:IContactServices
    {
        private readonly ContextClass context;
        private readonly ILogger<ContactServices> logger;
        private readonly IPasswordHasher hasher;
        public ContactServices(ContextClass context, ILogger<ContactServices> logger, IPasswordHasher hasher)
        {
            this.context = context;
            this.logger = logger;
            this.hasher = hasher;
        }

        public async Task<bool> AddContact(Contacts contact, string token)
        {
            try
            {
                var userId = hasher.TokenDecoder(token);
                contact.id = Guid.NewGuid().ToString();
                contact.createdBy = userId;
                contact.createdAt = DateTime.Now;
                contact.isSpam = false;
                await context.contactsTable.AddAsync(contact);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Contacts>> GetAllContacts(string type)
        {
            try
            {
                if(type == "spam")
                {
                    var contacts = await context.contactsTable.Where(x => x.isSpam == true).ToListAsync();
                    return contacts;    
                } else if(type == "all")
                {
                    var contacts = await context.contactsTable.ToListAsync();
                    return contacts;
                }
                logger.LogError("something went wrong");
                return null;
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> spamSwitch(string contactId)
        {
            try
            {
                var contact = await context.contactsTable.FirstOrDefaultAsync(x => x.id == contactId);
                if(contact == null)
                {
                    logger.LogError("contact doesnot exist");
                    return false;
                }
                if (contact.isSpam == true)
                {
                    contact.isSpam = false;
                    await context.SaveChangesAsync();
                    return true;
                } else if(contact.isSpam == false)
                {
                    contact.isSpam = true;
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
