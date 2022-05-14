using WebAPI.Models;

namespace WebAPI.Services
{
    public class MessageService : IMessageService
    {
        
        public List<Message> GetAllMessages()
        {
            throw new NotImplementedException();
        }

        public Message Get(string id)
        {
            throw new NotImplementedException();
        }

        public void Edit(string id, string body, string sentBy, string sentTo, DateTime dateCreated)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
