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

        private int invite(string from, string to, string server)
        {
            if (from == null || to == null) return 1;

            userService = new UserService(to);
            User user = userService.Get(to);
            if (user == null) return 1; // Checking if the user doesn't exist.

            contactService = new ContactService(to);
            Contact contact = contactService.Get(from);
            if (contact != null) return 1; // Checking if the contact already exists (as one of the user's contacts).

            chatService = new ChatService(to);
            if (chatService.CreateChat(from, from, server) > 0) return 1;

            return 0;
        }

        /// <summary>
        /// Sends an invitation to join a new chat (creates chat -> and then creates contact)
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] RequestOfNewInvitation request)
        {
            if (request == null) return;
            invite(request.From, request.To, request.Server);

            /*if (invite(request.From, request.To, request.Server) > 0)
            {
                Response.StatusCode = 404;
            } else
            {
                Response.StatusCode = 201;
            }*/

            /*string from = request.From;
            string to = request.To;

            if (from == null || to == null) return;

            userService = new UserService(to);
            User user = userService.Get(to);
            if (user == null)  // Checking if the user doesn't exist.
            {
                Response.StatusCode = 404;
                return;
            }

            // string id = request.To;

            contactService = new ContactService(to);
            Contact contact = contactService.Get(from);
            if (contact != null) // Checking if the contact already exists (as one of the user's contacts).
            {
                Response.StatusCode = 404;
                return;
            }

            chatService = new ChatService(to);
            int num = chatService.CreateChat(from, from, request.Server); // Creates a chat and a contact. The name of the contact is the same his/hers id.
            if (num > 0)
            {
                Response.StatusCode = 404;
                return;
            }*/

            // Response.StatusCode = 201;
        }
    }
}
