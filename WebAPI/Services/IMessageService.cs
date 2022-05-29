using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IMessageService
    {
        public List<Message> GetAllMessages(string id);
        public Message Get(string id1, string id2);
        public void Edit(string id, bool sent, string content = null, DateTime? created = null);
        public void Delete(string id);
        public void SendMessage(string id1, string id2, string content, bool sent);
    }
}
