using Contacts_App.Interfaces;
using Contacts_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contacts_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices services;
        public UserController(IUserServices services)
        {
            this.services = services;
        }

        [HttpPost]
        public async Task<IActionResult> newUser(Users user)
        {
            var response = await services.Register(user);
            if (response)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Users user)
        {
            var response = await services.Login(user);
            if(response!= null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var response = await services.GetAllUsers();
            if(response!= null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
