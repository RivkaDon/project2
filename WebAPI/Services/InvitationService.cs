using WebAPI.Models;

namespace WebAPI.Services
{
    public class InvitationService : IInvitationService
    {
        private IUserService userService;
        private IChatService chatService;
        private bool transfered = false;

        public InvitationService(IUserService userService, IChatService chatService)
        {
            this.chatService = chatService;
            this.userService = userService;
        }

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

            User user = userService.Get(to);
            if (user == null) return 1; // Checking if the user doesn't exist.

            if(userService.GetContact(to, from) != null)
            {
                return 1;
            }
            userService.CreateContact(to, from, from, server);
            chatService.CreateChat(to, from, from, server);
            return 0;
        }
    }
}
