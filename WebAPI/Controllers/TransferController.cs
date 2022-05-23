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
        private IUserService userService;
        private IChatService chatService;

        private IInvitationService invitationService = new InvitationService();

        /// <summary>
        /// Transfers a new message to one of the users.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] RequestTransferOfNewMessage request)
        {
            string from = request.From;
            string to = request.To;

            userService = new UserService(from);

            User user1 = userService.Get(from);
            if (user1 == null) return; // Checking if the user exists.

            User user2 = userService.Get(to);
            if (user2 == null) return;

            chatService = new ChatService(from);
            Chat chat = chatService.Get(to);

            if (chat == null) // Checking if the contact exists (as one of the user's contacts).
            {
                InvitationsController invitationsController = new InvitationsController();
                RequestOfNewInvitation r = invitationService.Create(from, to, "localhost:7104");

                invitationsController.Post(r); // Sending an invitation.
                // chatService.CreateChat(to, user2.Name, r.Server);
                chat = chatService.Get(to);
            }

            IMessageService messageService = new MessageService(chat, from);
            messageService.SendMessage(request.Content, false);
            //messageService.sendTo(request.)

            // update the second user
        }
    }
}
