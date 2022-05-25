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
                Server = server // maybe change that
            };
            return r;
        }
    }
}
