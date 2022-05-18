using WebAPI.Models;

namespace WebAPI.Services
{
    public class MessageService : IMessageService
    {
        private IChatService chatService = new ChatService();
        private Chat chat;
        /*private List<Message> messages = new List<Message>();*/

        public MessageService(Chat chat)
        {
            this.chat = chat;
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
            if (Exists(id))
            {
                Message message = Get(id);
                chat.Messages.Edit(message, sent, content, created);

                if (sent)
                {
                    chat.Contact.Last = message.Content;
                    chat.Contact.LastDate = message.Created;
                }
            }
        }

        public void Delete(string id)
        {
            if (Exists(id))
            {
                Message message = Get(id);
                chatService.DeleteMessage(chat, message);
            }
        }

        public void SendMessage(string content, bool sent)
        {
            if (content != null)
            {
                Message message = new Message();
                int len = GetAllMessages().Count;

                message.Id = len.ToString();
                message.Created = DateTime.Now;
                message.Content = content;
                message.Sent = sent;

                chat.Messages.Add(message);

                if (sent)
                {
                    chat.Contact.Last = message.Content;
                    chat.Contact.LastDate = message.Created;
                }
            }
        }
    }
}
