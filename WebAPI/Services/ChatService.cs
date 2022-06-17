using WebAPI.Models;

namespace WebAPI.Services
{
    public class ChatService : IChatService
    {
        private IUserService userService;
        private static ChatList chatList = new ChatList();
        //private User user;

        public ChatService(IUserService userService)
        {
            this.userService = userService;
        }

        public List<Chat> GetAllChats(string id)
        {
            List<Chat> chatList = new List<Chat>();
            foreach(Chat chat in ChatService.chatList.Chats)
            {
                if(chat.ContactIdA == id || chat.ContactIdB == id)
                {
                    chatList.Add(chat);
                }
            }
            if(chatList.Count() == 0)
            {
                return null;
            }
            return chatList;
        }

        public List<Message> GetMessagesWith(string id, string contactId)
        {
            Chat c = Get(id, contactId);
            if (c != null)
            {
                return c.Messages.Messages;
            }
            return null;
        }


        public bool Exists(string id1,string id2)
        {
            if (string.IsNullOrEmpty(id1) || string.IsNullOrEmpty(id2)) return false;

            foreach(Chat chat in chatList.Chats)
            {
                if ((chat.ContactIdA == id1 && chat.ContactIdB ==id2) || (chat.ContactIdA == id2 && chat.ContactIdB == id1))
                {
                    return true;
                }
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
            foreach (Chat chat in chatList.Chats)
            {
                if ((chat.ContactIdA == id1 && chat.ContactIdB == id2) || (chat.ContactIdA == id2 && chat.ContactIdB == id1))
                {
                    return chat;
                }
            }
            return null;
        }

        public List<Message> GetMessageBetween(string id, string contactId)
        {
            if (id == null || contactId == null) return null;
            if (!Exists(id, contactId)) return null;
            return Get(id, contactId).Messages.Messages;
        }

        /*public int Edit(string id, Contact contact = null, MessageList messageList = null)
        {
            Chat chat = Get(Global.Id, id);
            if (chat != null)
            {
                User user = userService.Get(Global.Id);
                user.Chats.Edit(chat, contact, messageList);
                return 0;
            }
            return 1;
        }*/

        /*public int Delete(string id1, string id2)
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
        }*/

        public int CreateChat(string id1, string id2, string name, string server)
        {

            if (id1 == null || id2 == null) return 1;
            if (Exists(id1, id2)) return 1;

            if (!userService.ContactExists(id1, id2) && !userService.ContactExists(id2, id1))
            {
                return 0;
            } else
            {
                ChatService.chatList.Add(new Chat(DateTime.UtcNow.ToString() + id1 + id2, id1, id2, new MessageList()));
                return 1;
            }
        }

        /*public int CreateChatInvitation(string id1, string id2, string name, string server)
        {
            if (id1 == null || id2 == null) return 1;
            if (id1 == id2) return 1;

            if (Exists(id1, id2)) return 1;

            User user = userService.Get(Global.Id);
            int num = userService.CreateChatInvitation(id1, id2, name, server);

            if (num > 0) return num;

            userService.CreateChatInvitation(id2, id1, user.Name, server);

            
            return 0;
        }*/

        public int CreateMessage(string id1, string id2, string content)
        {
            if (content == null) return 0;
            Chat chat = Get(id1, id2);
            if (chat == null)
            {
                return 0;
            }
            
            //chat.Messages.Add(message);
            DateTime date = DateTime.Now;
            var random = new Random();
            int randNum = random.Next(0, 999);
            Message message = new Message("" + date + randNum, content, date, id1, id2);

            chat.Messages.Add(message);
            userService.EditContact(id1, id2, null, null, content, message.Created);
            userService.EditContact(id2, id1, null, null, content, message.Created);

            return 1;
        }

        public Message GetMessageById(string id, string contactId, string messageId)
        {
            if (id == null || contactId == null || messageId == null) return null;
            Chat chat = Get(id, contactId);
            foreach(Message m in chat.Messages.Messages)
            {
                if (m.Id == messageId)
                {
                    return m;
                }
            }
            return null;
        }

        public void EditMessageById(string id, string contactId, string messageId, string content)
        {
            if (id == null || contactId == null || messageId == null) return;
            Chat chat = Get(id, contactId);
            Message message = null;
            foreach (Message m in chat.Messages.Messages)
            {
                if (m.Id == messageId)
                {
                    message = m;
                    break;
                }
            }
            if(message != null)
            {
                message.Content = content;
                DateTime date = DateTime.Now;
                message.Created = date;
                userService.EditContact(id, contactId, null, null, content, date);
                userService.EditContact(contactId, id, null, null, content, date);
            }
        }


        public int DeleteMessage(string id, string contactId, string messageId)
        {
            if (id == null || contactId == null || messageId == null) return 1;

            MessageList messageList = Get(id, contactId).Messages;
            if (messageList == null) return 1;
            Message message = null;
            foreach(Message m in messageList.Messages)
            {
                if(m.Id == messageId)
                {
                    message = m;
                    break;
                }
            }
            if (message != null)
            {
                messageList.Remove(message);
                return 0;
            }
            return 1;
        }
        
        
    }
}
