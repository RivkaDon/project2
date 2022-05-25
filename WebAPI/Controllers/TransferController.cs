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

            userService = new UserService(to);

            User user1 = userService.Get(to);
            if (user1 == null) // Checking if the user exists.
            {
                Response.StatusCode = 404;
                return;
            }

            /*User user2 = userService.Get(from);
            if (user2 == null)
            {
                Response.StatusCode = 404;
                return;
            }*/

            chatService = new ChatService(to);
            Chat chat = chatService.Get(from);

            if (chat == null) // Checking if the contact exists (as one of the user's contacts).
            {
                InvitationsController invitationsController = new InvitationsController();
                RequestOfNewInvitation r = invitationService.Create(from, to, "localhost:7105");

                invitationsController.Post(r); // Sending an invitation.
                if (!invitationsController.invited)
                {
                    Response.StatusCode = 404;
                    return;
                }
                invitationsController.invited = false;

                // chatService.CreateChat(to, user2.Name, r.Server);
                chat = chatService.Get(from);
            }

            IMessageService messageService = new MessageService(chat, to);
            messageService.SendMessage(request.Content, true);

            /*if (to == Global.Id)
            {
                messageService.SendMessage(request.Content, true);
            } else
            {
                messageService.SendMessage(request.Content, false);
            }*/


            Response.StatusCode = 201;

            // update the second user
        }
    }
}
