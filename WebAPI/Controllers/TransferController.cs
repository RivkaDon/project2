using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private IUserService userService = new UserService();
        private IContactService contactService = new ContactService();
        private IMessageService messageService;

        // POST api/<TransferController>
        /// <summary>
        /// Transfers a new message to one of the users.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] RequestTransferOfNewMessage request)
        {
            /*User user = userService.Get(request.From);
            if (user == null) return; // Checking if the user exists.

            Contact contact = contactService.Get(request.To);
            if (contact == null)
            {
                // send invite
            }
            
            messageService = new MessageService(contact);
            messageService.SendMessage(request.Content, false); // check this*/
        }
    }
}
