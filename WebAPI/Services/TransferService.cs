using WebAPI.Controllers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class TransferService : ITransferService
    {
        // private IUserService userService;
        private IChatService chatService;
        // private IMessageService messageService;

        private IInvitationService invitationService = new InvitationService();

        public TransferService(string id)
        {
            chatService = new ChatService(id);
            /*Chat c = chatService.Get(id);
            messageService = new MessageService(c);*/
        }

        public void transfer(User from, User to, string content)
        {
            if (from == null || to == null) return; // 404?
            if (string.IsNullOrEmpty(content)) return;

            chatService = new ChatService(from.Id);
            Chat chat = chatService.Get(to.Id);

            if (chat == null) // Checking if the contact exists (as one of the user's contacts).
            {
                InvitationsController invitationsController = new InvitationsController();
                RequestOfNewInvitation r = invitationService.Create(from.Id, to.Id, "localhost:7104");

                invitationsController.Post(r); // Sending an invitation.
                chatService.CreateChat(to.Id, to.Name, r.Server);
                
            }

        }
    }
}
