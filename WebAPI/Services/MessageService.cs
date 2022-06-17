using WebAPI.Models;

namespace WebAPI.Services
{
    public class MessageService : IMessageService
    {
        private IChatService chatService;
        private List<Message> messages;
        //private Chat chat;

        public MessageService(IChatService chatService)
        {
            this.chatService = chatService;
            this.messages = new List<Message>();
        }

        public MessageService(IChatService chatService, List<Message> list)
        {
            this.chatService = chatService;
            this.messages = list;
        }

        public List<Message> GetAllMessages()
        {
            return messages;
        }

        public List<Message> getMessageByIds(string sId, string rId)
        {
            List<Message> messages = new List<Message>();
            foreach(Message m in messages)
            {
                if((m.senderId == sId && m.receiverId == rId) || (m.senderId == rId && m.receiverId == sId))
                {
                    messages.Add(m);
                }
            }
            return messages;
        }

        public int Count(string sId, string rId)
        {
            return getMessageByIds(sId, rId).Count;
        }

        /*public string lastId(string userId, string contactId)
        {
            List<Message> messages = GetAllMessages(userId, contactId);
            int len = messages.Count;
            if (len > 0)
            {
                return messages[len - 1].Id;
            }
            return "";
        }*/

        public bool Exists(string sId, string rId)
        {
            return Count(sId, rId) > 0;
        }

        public List<Message> messagesById(string id)
        {
            List<Message> messages = new List<Message>();
            foreach(Message m in this.messages)
            {
                if(m.senderId == id || m.receiverId== id)
                {
                    messages.Add(m);
                }
            }
            return messages;
        }

        public Message Get(string id1, string id2)
        {
            List<Message> messages = messagesById(id1);
            foreach(Message m in messages)
            {
                if(m.Id == id2)
                {
                    return m;
                }
            }
            return null;
        }

        public List<Message> GetAllMessages(string userId, string contactId)
        {
            throw new NotImplementedException();
        }

        public void Edit(string id, bool sent, string content = null, DateTime? created = null)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string id1, string id2, string content, bool sent)
        {
            throw new NotImplementedException();
        }
    }
}
