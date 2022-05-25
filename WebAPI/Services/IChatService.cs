using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IChatService
    {
        public List<Chat> GetAllChats();
        public Chat Get(string id);
        public void Edit(string id, Contact contact = null, MessageList messageList = null);
        public void Delete(string id);
        public int CreateChat(string id, string name, string server);
        public int CreateChatInvitation(string id, string name, string server);
        public int CreateMessage(Chat chat, Message message);
        public int DeleteMessage(Chat chat, Message message);
    }
}
