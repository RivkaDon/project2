using WebAPI.Models;

namespace WebAPI.Services
{
    public class InvitationService : IInvitationService
    {
        public RequestOfNewInvitation Create(string from, string to, string server)
        {
            RequestOfNewInvitation r = new RequestOfNewInvitation()
            {
                From = from,
                To = to,
                Server = "localhost:7104" // maybe change that
            };
            return r;
        }
    }
}
