using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    /*[Authorize]*/
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private IUserService userService;
        private IChatService chatService;
        private IContactService contactService;

        /// <summary>
        /// Sends an invitation to join a new chat (creates chat -> and then creates contact)
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] RequestOfNewInvitation request)
        {
            userService = new UserService(request.From);
            User user = userService.Get(request.From);
            if (user == null)  // Checking if the user exists.
            {
                Response.StatusCode = 404;
                return;
            }

            string id = request.To;

            contactService = new ContactService(request.From);
            Contact contact = contactService.Get(id);
            if (contact != null) // Checking if the contact already exists (as one of the user's contacts).
            {
                Response.StatusCode = 404;
                return;
            }

            chatService = new ChatService(request.From);
            int num = chatService.CreateChat(id, id, request.Server); // Creates a chat and a contact. The name of the contact is the same his/hers id.
            if (num > 0)
            {
                Response.StatusCode = 404;
            }
            Response.StatusCode = 201;
        }
    }
}
