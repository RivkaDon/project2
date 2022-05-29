using WebAPI.Models;

namespace WebAPI.Services
{
    public class MessageService : IMessageService
    {
        private IChatService chatService;
        private Chat chat;

        public MessageService(Chat c)
        {
            chatService = new ChatService();
            chat = c;
        }
        
        public List<Message> GetAllMessages()
        {
            if (chat == null) return null;
            if (chat.Messages == null) return null;
            return chat.Messages.Messages;
        }

        public int Count()
        {
            return GetAllMessages().Count;
        }

        public string lastId()
        {
            List<Message> messages = GetAllMessages();
            int len = messages.Count;
            if (len > 0)
            {
                return messages[len - 1].Id;
            }
            return "";
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;
            
            List<Message> messages = GetAllMessages();
            if (messages == null) return false;

            foreach (Message message in messages)
            {
                if (message.Id == id) return true;
            }
            return false;
        }

        public Message Get(string id)
        {
            if (!Exists(id)) return null;
            return GetAllMessages().Find(x => x.Id == id);
        }

        public void Edit(string id, bool sent, string content = null, DateTime? created = null)
        {
            Message message = Get(id);
            if (message != null)
            {
                chat.Messages.Edit(message, sent, content, created);

                // Checking is the message was sent by the user (and not to the user).
                chat.Contact.Last = message.Content;
                chat.Contact.LastDate = message.Created;
            }
        }

        public void Delete(string id)
        {
            Message message = Get(id);
            if (message != null)
            {
                chatService.DeleteMessage(chat, message);
            }
        }

        public void SendMessage(string content, bool sent)
        {
            if (content != null)
            {
                Message message = new Message();
                int len = GetAllMessages().Count;

                message.Id = lastId() + len;
                message.Created = DateTime.Now;
                message.Content = content;
                message.Sent = sent;

                chat.Messages.Add(message);

                // Checking if the message was sent by the user (and not to the user).
                chat.Contact.Last = message.Content;
                chat.Contact.LastDate = message.Created;
            }
        }
    }
}
