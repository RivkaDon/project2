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
        private IInvitationService invitationService;

        public TransferController(IUserService us, IChatService cs, IInvitationService iService)
        {
            Global.Server = "localhost:7105";
            userService = us;
            chatService = cs;
            invitationService = iService;
        }

        private int updateChat(string from, string to, string content)
        {
            Chat c = chatService.Get(from, to);

            if (c != null && content != null)
            {
                chatService.CreateMessage(from, to, content); // or from?
                return 0;
            }
            if (!userService.Exists(from)) return 0;
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

            User user1 = userService.Get(to);
            if (user1 == null) // Checking if the user exists.
            {
                Response.StatusCode = 404;
                return;
            }

            Chat chat = chatService.Get(to, from); // check this

            if (chat == null) // Checking if the contact exists (as one of the user's contacts).
            {
                InvitationsController invitationsController = new InvitationsController(invitationService);
                RequestOfNewInvitation r = invitationService.Create(from, to, Global.Server);

                invitationsController.Post(r); // Sending an invitation.
                if (!invitationsController.invited)
                {
                    Response.StatusCode = 404;
                    return;
                }
                invitationsController.invited = false;
            }
            if (chatService.CreateMessage(from, to, request.Content) == 0) { 
                Response.StatusCode = 404;
                return; 
            }
            Response.StatusCode = 201;
        }
    }
}
