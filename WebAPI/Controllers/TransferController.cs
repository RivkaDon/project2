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

        private int updateChat(string from, string to, string content)
        {
            chatService = new ChatService(from);
            Chat c = chatService.Get(to);

            if (c != null && content != null)
            {
                IMessageService messageService = new MessageService(c);
                messageService.SendMessage(content, false);
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// Transfers a new message to one of the users.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] RequestTransferOfNewMessage request)
        {
            string from = request.From;
            string to = request.To;

            if (from == to)
            {
                Response.StatusCode = 404;
                return;
            }

            userService = new UserService(to);

            User user1 = userService.Get(to);
            if (user1 == null) // Checking if the user exists.
            {
                Response.StatusCode = 404;
                return;
            }

            chatService = new ChatService(to);
            Chat chat = chatService.Get(from);

            if (chat == null) // Checking if the contact exists (as one of the user's contacts).
            {
                InvitationsController invitationsController = new InvitationsController();
                RequestOfNewInvitation r = invitationService.Create(from, to, Global.Id);

                invitationsController.Post(r); // Sending an invitation.
                if (!invitationsController.invited)
                {
                    Response.StatusCode = 404;
                    return;
                }
                invitationsController.invited = false;
                chat = chatService.Get(from);
            }

            IMessageService messageService = new MessageService(chat, to);
            messageService.SendMessage(request.Content, true);

            if (updateChat(from, to, request.Content) > 0)
            {
                Response.StatusCode = 404;
                return;
            }

            Response.StatusCode = 201;

            // update the second user
        }
    }
}
