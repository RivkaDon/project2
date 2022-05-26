using WebAPI.Models;

namespace WebAPI.Services
{
    public class ChatService : IChatService
    {
        private IUserService userService;
        private User user;

        public ChatService()
        {
            userService = new UserService();
            user = userService.Get(Global.Id);
        }

        public ChatService(string id)
        {
            userService = new UserService(id);
            user = userService.Get(id);
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

        public int Edit(string id, Contact contact = null, MessageList messageList = null)
        {
            Chat chat = Get(id);
            if (chat != null)
            {
                user.Chats.Edit(chat, contact, messageList);
                return 0;
            }
            return 1;
        }

        public int Delete(string id)
        {
            Chat chat = Get(id);
            if (chat != null)
            {
                userService.DeleteChat(chat); // add else return 1 for 404

                IUserService us = new UserService(id);
                User u = us.Get(id);
                List<Chat> chats = u.Chats.Chats;
                Chat c = chats.Find(c => c.Id == user.Id);
                us.DeleteChat(c);

                return 0;
            }

            return 1;
        }

        public int CreateChat(string id, string name, string server)
        {
            if (Exists(id)) return 1;
            if (id == user.Id) return 1;
            int num = userService.CreateChat(id, name, server);

            if (num > 0) return num;

            IUserService us = new UserService(id);
            us.CreateChat(user.Id, user.Name, Global.Server);
            return 0;
        }

        public int CreateChatInvitation(string id, string name, string server)
        {
            if (Exists(id)) return 1;
            if (id == user.Id) return 1;
            int num = userService.CreateChatInvitation(id, name, server);

            if (num > 0) return num;

            IUserService us = new UserService(id);
            us.CreateChatInvitation(user.Id, user.Name, Global.Server);
            return 0;
        }

        public int CreateMessage(Chat chat, Message message)
        {
            if (message == null) return 1;

            MessageList messageList = GetMessageList(chat);
            if (messageList == null) return 1;
            if (!messageList.Messages.Contains(message)) return 1;

            messageList.Add(message);
            return 0;
        }

        public int DeleteMessage(Chat chat, Message message)
        {
            if (message == null) return 1;

            MessageList messageList = chat.Messages;
            if (messageList == null) return 1;
            if (!messageList.Messages.Contains(message)) return 1;

            messageList.Remove(message);
            return 0;
        }
    }
}
