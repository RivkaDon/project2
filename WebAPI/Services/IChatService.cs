using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IChatService
    {
        public List<Chat> GetAllChats();
        public Chat Get(string id);
        public void Edit(string id, Contact contact = null, MessageList messageList = null);
        public void Delete(string id);
        public void CreateChat(string id, string name, string server);
        public void CreateMessage(Chat chat, Message message);
        public void DeleteMessage(Chat chat, Message message);
    }
}
