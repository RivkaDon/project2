using WebAPI.Models;

namespace WebAPI.Services
{
    public class MessageService : IMessageService
    {
        private IChatService chatService;
        //private Chat chat;

        public MessageService()
        {
            chatService = new ChatService();
            //chat = c;
        }

        public List<Message> GetAllMessages(string userId, string contactId)
        {
            Chat chat = chatService.Get(userId, contactId);
            if (chat == null) return null;
            if (chat.Messages == null) return null;
            return chat.Messages.Messages;
        }

        public int Count(string id)
        {
            return GetAllMessages(Global.Id, id).Count;
        }

        public string lastId(string userId, string contactId)
        {
            List<Message> messages = GetAllMessages(userId, contactId);
            int len = messages.Count;
            if (len > 0)
            {
                return messages[len - 1].Id;
            }
            return "";
        }

        public bool Exists(string id1, string id2)
        {
            if (string.IsNullOrEmpty(id1) || string.IsNullOrEmpty(id2)) return false;

            List<Message> messages = GetAllMessages(Global.Id, id1);
            if (messages == null) return false;

            foreach (Message message in messages)
            {
                if (message.Id == id2) return true;
            }
            return false;
        }

        public Message Get(string id1, string id2)
        {
            if (!Exists(id1, id2)) return null;
            return GetAllMessages(Global.Id, id1).Find(x => x.Id == id2);
        }

        public void Edit(string id, bool sent, string content = null, DateTime? created = null)
        {
            Chat chat = chatService.Get(Global.Id, id);
            Message message = Get(Global.Id, id);
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
            Chat chat = chatService.Get(Global.Id, id);
            Message message = Get(Global.Id, id);
            if (message != null)
            {
                chatService.DeleteMessage(chat, message);
            }
        }

        public void SendMessage(string id1, string id2, string content, bool sent)
        {
            if (content != null)
            {
                Chat chat = chatService.Get(id1, id2);

                Message message = new Message();
                //int len = GetAllMessages(id2, id1).Count; // 

                DateTime date = DateTime.Now;

                var random = new Random();
                int randNum = random.Next(0, 999);

                message.Id = "" + date + randNum;
                message.Created = date;
                message.Content = content;
                message.Sent = sent;

                chat.Messages.Add(message);


                // Checking if the message was sent by the user (and not to the user).
                chat.Contact.Last = message.Content;
                chat.Contact.LastDate = message.Created;

                Contact contact = chatService.GetContact(id1, id2);
                contact.Last = message.Content;
                contact.LastDate = message.Created;

                //chatService.CreateMessage(id1, chat, message);
            }
        }
    }
}
