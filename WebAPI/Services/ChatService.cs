using WebAPI.Models;

namespace WebAPI.Services
{
    public class ChatService : IChatService
    {
        private IUserService userService = new UserService();
        private static User user;

        /*private static ChatList chats = new ChatList();*/

        public ChatService()
        {
            user = userService.Get(Global.Id);
        }

        public List<Chat> GetAllChats()
        {
            if (user == null) return null;
            if (user.Chats == null) return null;
            return user.Chats.Chats;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;

            List<Chat> chats = GetAllChats();
            if (chats == null) return false;

            foreach (var chat in chats)
            {
                if (chat.Id == id) return true;
            }
            return false;
        }

        public Chat Get(string id)
        {
            if (!Exists(id)) return null;
            return GetAllChats().Find(e => e.Id == id);
        }

        public MessageList GetMessageList(Chat chat)
        {
            if (chat == null) return null;
            if (!GetAllChats().Contains(chat)) return null;
            return chat.Messages;
        }

        public void Edit(string id, Contact contact = null, MessageList messageList = null)
        {
            Chat chat = Get(id);
            if (chat != null)
            {
                user.Chats.Edit(chat, contact, messageList);
            }
        }

        public void Delete(string id)
        {
            Chat chat = Get(id);
            if (chat != null)
            {
                userService.DeleteChat(chat);
            }
        }

        public void CreateChat(string id, string name, string server)
        {
            if (Exists(id)) return;
            userService.CreateChat(id, name, server);
        }

        public void CreateMessage(Chat chat, Message message)
        {
            if (message == null) return;

            MessageList messageList = GetMessageList(chat);
            if (messageList == null) return;
            if (!messageList.Messages.Contains(message)) return;

            messageList.Add(message);
        }

        public void DeleteMessage(Chat chat, Message message)
        {
            if (message == null) return;

            MessageList messageList = GetMessageList(chat);
            if (messageList == null) return;
            if (!messageList.Messages.Contains(message)) return;

            messageList.Remove(message);
        }
    }
}
