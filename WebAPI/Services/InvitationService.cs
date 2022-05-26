using WebAPI.Models;

namespace WebAPI.Services
{
    public class InvitationService : IInvitationService
    {
        private IUserService userService;
        private IChatService chatService;
        private IContactService contactService;
        private bool transfered = false;

        public void Transfer()
        {
            transfered = true;
        }

        public bool IsTransfered()
        {
            return transfered;
        }

        public RequestOfNewInvitation Create(string from, string to, string server)
        {
            RequestOfNewInvitation r = new RequestOfNewInvitation()
            {
                From = from,
                To = to,
                Server = server
            };
            return r;
        }

        public int invite(string from, string to, string server)
        {
            if (from == null || to == null) return 1;

            userService = new UserService(to);
            User user = userService.Get(to);
            if (user == null) return 1; // Checking if the user doesn't exist.

            contactService = new ContactService(to);
            Contact contact = contactService.Get(from);
            if (contact != null) return 1; // Checking if the contact already exists (as one of the user's contacts).

            chatService = new ChatService(to);
            if (chatService.CreateChatInvitation(from, from, server) > 0) return 1;

            return 0;
        }
    }
}
