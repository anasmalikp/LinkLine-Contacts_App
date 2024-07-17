using Contacts_App.Interfaces;
using Contacts_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contacts_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactServices services;
        public ContactController(IContactServices services)
        {
            this.services = services;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNewContact(Contacts contact, string token)
        {
            if(contact == null||token == null)
            {
                return BadRequest();
            }
            var response = await services.AddContact(contact, token);
            if (response)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> SwitchSpam(string contactId)
        {
            var response = await services.spamSwitch(contactId);
            if (response)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(string type, int page)
        {
            var response = await services.GetAllContacts(type);
            var contactsPerPage = response.Skip((page - 1) * 5).Take(5).ToList();
            if (response == null || contactsPerPage == null)
            {
                return BadRequest();
            }
            return Ok(contactsPerPage);
        }
    }
}
