using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IChatService
    {
        public List<Chat> GetAllChats(string id);
        public List<Message> GetMessageBetween(string id, string contactId);
        public Chat Get(string id1, string id2);
        public Message GetMessageById(string id, string contactId, string messageId);
        public void EditMessageById(string id, string contactId, string messageId, string content);
        public bool Exists(string id1, string id2);
        public int CreateChat(string id1, string id2, string name, string server);
        public int CreateMessage(string id1, string id2, string message);
        public int DeleteMessage(string id, string contactId, string message);
    }
}
