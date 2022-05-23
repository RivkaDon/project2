using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IInvitationService
    {
        public RequestOfNewInvitation Create(string from, string to, string server);
    }
}
