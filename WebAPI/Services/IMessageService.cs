using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IMessageService
    {
        public List<Message> GetAllMessages();
        public Message Get(string id);
        public void Edit(string id, bool sent, string content = null, DateTime? created = null);
        public void Delete(string id);
        // public void sendTo(string id, string content);
        public void SendMessage(string content, bool sent);
    }
}
