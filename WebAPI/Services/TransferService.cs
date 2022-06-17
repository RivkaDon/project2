using WebAPI.Controllers;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class TransferService : ITransferService
    {
        private IChatService chatService;
        private IInvitationService invitationService;

        public TransferService(IChatService chatService, IInvitationService invitationService)
        {
            this.chatService = chatService;
            this.invitationService = invitationService;
        }

        public void transfer(User from, User to, string content)
        {
            if (from == null || to == null) return;
            if (string.IsNullOrEmpty(content)) return;

            Chat chat = chatService.Get(from.Id, to.Id);

            if (chat == null) // Checking if the contact exists (as one of the user's contacts).
            {
                InvitationsController invitationsController = new InvitationsController(invitationService);
                RequestOfNewInvitation r = invitationService.Create(from.Id, to.Id, "localhost:7105");

                invitationsController.Post(r); // Sending an invitation.
                chatService.CreateChat(from.Id, to.Id, to.Name, r.Server);
            }

            chatService.CreateMessage(from.Id, to.Id, content);
        }
    }
}
