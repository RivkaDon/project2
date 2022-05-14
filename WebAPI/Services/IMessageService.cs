using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IMessageService
    {
        public List<Message> GetAllMessages();
        public Message Get(string id);
        public void Edit(string id, string body, string sentBy, string sentTo, DateTime dateCreated);
        public void Delete(string id);
        void SendMessage(string message);
    }
}
