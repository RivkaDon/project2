using WebAPI.Models;

namespace WebAPI.Services
{
    public class ChatService : IChatService
    {
        private IUserService userService;
        //private User user;

        public ChatService()
        {
            userService = new UserService();
            //user = userService.Get(id);
        }

        public List<Chat> GetAllChats(string id)
        {
            User user = userService.Get(id);
            if (user == null) return null;
            if (user.Chats == null) return null;
            return user.Chats.Chats;
        }

        public bool Exists(string id1,string id2)
        {
            if (string.IsNullOrEmpty(id1) || string.IsNullOrEmpty(id2)) return false;

            List<Chat> chats = GetAllChats(id1);
            if (chats == null) return false;

            foreach (var chat in chats)
            {
                if (chat.Id == id2) return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the contact with id2 (from id1's contacts list).
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        public Chat Get(string id1, string id2)
        {
            if (!Exists(id1, id2)) return null;
            return GetAllChats(id1).Find(e => e.Id == id2);
        }

        public MessageList GetMessageList(string id, Chat chat)
        {
            if (chat == null) return null;
            if (!GetAllChats(id).Contains(chat)) return null;
            return chat.Messages;
        }

        public int Edit(string id, Contact contact = null, MessageList messageList = null)
        {
            Chat chat = Get(Global.Id, id);
            if (chat != null)
            {
                User user = userService.Get(Global.Id);
                user.Chats.Edit(chat, contact, messageList);
                return 0;
            }
            return 1;
        }

        public int Delete(string id1, string id2)
        {
            Chat chat = Get(id1, id2);
            if (chat != null)
            {
                userService.DeleteChat(id1, chat); // add else return 1 for 404
                
                User u = userService.Get(id1);
                if (u != null)
                {
                    List<Chat> chats = u.Chats.Chats;
                    User user = userService.Get(id2);
                    Chat c = chats.Find(c => c.Id == user.Id);
                    userService.DeleteChat(id2, c);
                }
                return 0;
            }
            return 1;
        }

        public int CreateChat(string id1, string id2, string name, string server)
        {
            if (id1 == null || id2 == null) return 1;
            if (id1 == id2) return 1;

            if (Exists(id1, id2)) return 1;

            User user = userService.Get(id1);
            int num = userService.CreateChat(id1, id2, name, server);

            if (num > 0) return num;

            userService.CreateChat(id2, id1, user.Name, server);
            /*IUserService us = new UserService();
            us.CreateChat(user.Id, user.Name, Global.Server);*/
            return 0;
        }

        public int CreateChatInvitation(string id1, string id2, string name, string server)
        {
            if (id1 == null || id2 == null) return 1;
            if (id1 == id2) return 1;

            if (Exists(id1, id2)) return 1;

            User user = userService.Get(Global.Id);
            int num = userService.CreateChatInvitation(id1, id2, name, server);

            if (num > 0) return num;

            userService.CreateChatInvitation(id2, id1, user.Name, server);

            /*IUserService us = new UserService();
            us.CreateChatInvitation(user.Id, user.Name, Global.Server);*/
            return 0;
        }

        public int CreateMessage(string id, Chat chat, Message message)
        {
            if (message == null) return 1;

            MessageList messageList = GetMessageList(id, chat);
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
