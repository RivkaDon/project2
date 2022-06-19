using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class ChatServiceDB : IChatService
    {
        WebAPIContext _context;
        private IUserService userService;
        private static List<Chat> chats;

        public async Task InitializeChats()
        {
            using (var db = _context)
            {
                chats = await db.Chats.Include(x => x.Messages).AsNoTracking().ToListAsync();
                //chats = await db.Chats.ToListAsync();
                if (chats.Count == 0)
                {
                    chats = new ChatList().Chats;
                    foreach (Chat chat in chats)
                    {
                        db.Chats.Add(chat);
                        db.SaveChanges();

                        //_context.ChangeTracker.Clear();
                    }

                }
            }
        }

        public ChatServiceDB(WebAPIContext context, IUserService userService)
        {
            _context = context;
            this.userService = userService;
        }

        public int CreateChat(string id1, string id2, string name, string server)
        {
            if (id1 == null || id2 == null) return 1;
            if (Exists(id1, id2)) return 1;

            if (!userService.ContactExists(id1, id2) && !userService.ContactExists(id2, id1))
            {
                return 0;
            }
            else
            {
                Chat chat = new Chat(DateTime.UtcNow.ToString() + id1 + id2, id1, id2, new MessageList());
                chats.Add(chat);
                _context.Chats.Add(chat);
                _context.SaveChanges();
                return 1;
            }
        }

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
            _context.Message.Add(message);
            _context.SaveChanges();
            return 1;
        }

        public int DeleteMessage(string id, string contactId, string messageId)
        {
            if (id == null || contactId == null || messageId == null) return 1;

            MessageList messageList = Get(id, contactId).Messages;
            if (messageList == null) return 1;
            Message message = null;
            foreach (Message m in messageList.Messages)
            {
                if (m.Id == messageId)
                {
                    message = m;
                    break;
                }
            }
            if (message != null)
            {
                messageList.Remove(message);
                _context.Message.Remove(message);
                _context.SaveChanges();
                return 0;
            }
            return 1;
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
            if (message != null)
            {
                message.Content = content;
                DateTime date = DateTime.Now;
                message.Created = date;
                userService.EditContact(id, contactId, null, null, content, date);
                userService.EditContact(contactId, id, null, null, content, date);
                _context.Message.Update(message);
                _context.SaveChanges();
            }
        }

        public bool Exists(string id1, string id2)
        {
            if (string.IsNullOrEmpty(id1) || string.IsNullOrEmpty(id2)) return false;

            foreach (Chat chat in chats)
            {
                if ((chat.ContactIdA == id1 && chat.ContactIdB == id2) || (chat.ContactIdA == id2 && chat.ContactIdB == id1))
                {
                    return true;
                }
            }
            return false;
        }

        public Chat Get(string id1, string id2)
        {
            foreach (Chat chat in chats)
            {
                if ((chat.ContactIdA == id1 && chat.ContactIdB == id2) || (chat.ContactIdA == id2 && chat.ContactIdB == id1))
                {
                    return chat;
                }
            }
            return null;
        }

        public List<Chat> GetAllChats(string id)
        {
            List<Chat> chatList = new List<Chat>();
            foreach (Chat chat in chats)
            {
                if (chat.ContactIdA == id || chat.ContactIdB == id)
                {
                    chatList.Add(chat);
                }
            }
            if (chatList.Count() == 0)
            {
                return null;
            }
            return chatList;
        }

        public List<Message> GetMessageBetween(string id, string contactId)
        {
            if (id == null || contactId == null) return null;
            if (!Exists(id, contactId)) return null;
            return Get(id, contactId).Messages.Messages;
        }

        public Message GetMessageById(string id, string contactId, string messageId)
        {
            if (id == null || contactId == null || messageId == null) return null;
            Chat chat = Get(id, contactId);
            foreach (Message m in chat.Messages.Messages)
            {
                if (m.Id == messageId)
                {
                    return m;
                }
            }
            return null;
        }
    }
}
