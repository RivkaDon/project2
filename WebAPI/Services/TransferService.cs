using WebAPI.Controllers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class TransferService : ITransferService
    {
        private IChatService chatService;
        private IInvitationService invitationService = new InvitationService();

        public TransferService(string id)
        {
            chatService = new ChatService();
        }

        public void transfer(User from, User to, string content)
        {
            if (from == null || to == null) return;
            if (string.IsNullOrEmpty(content)) return;

            chatService = new ChatService();
            Chat chat = chatService.Get(to.Id);

            if (chat == null) // Checking if the contact exists (as one of the user's contacts).
            {
                InvitationsController invitationsController = new InvitationsController();
                RequestOfNewInvitation r = invitationService.Create(from.Id, to.Id, "localhost:7105");

                invitationsController.Post(r); // Sending an invitation.
                chatService.CreateChat(to.Id, to.Name, r.Server);
            }
        }
    }
}
