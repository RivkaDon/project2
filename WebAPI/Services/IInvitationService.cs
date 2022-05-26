using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IInvitationService
    {
        public void Transfer();
        public bool IsTransfered();
        public RequestOfNewInvitation Create(string from, string to, string server);
        public int invite(string from, string to, string server);
    }
}
