using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ITransferService
    {
        public void transfer(User from, User to, string content);
    }
}
