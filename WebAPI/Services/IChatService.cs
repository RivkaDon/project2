using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IChatService
    {
        public List<Chat> GetAllChats(string id);
        public Contact GetContact(string userId, string contactId);
        public Chat Get(string id1, string id2);
        public int Edit(string id, Contact contact = null, MessageList messageList = null);
        public int Delete(string id1, string id2);
        public int CreateChat(string id1, string id2, string name, string server);
        public int CreateChatInvitation(string id1, string id2, string name, string server);
        public int CreateMessage(string id, Chat chat, Message message);
        public int DeleteMessage(Chat chat, Message message);
    }
}
