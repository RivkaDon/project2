using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IContactService
    {
        public List<Contact> GetAllContacts();
        public Contact Get(string id);
        public int Edit(string id, string name = null, string server = null, string last = null, DateTime? lastDate = null);
        public int Delete(string id);
        public void CreateContact(string id, string name, string server);
        public void UpdateLastDate(string id, List<Message> messages);
    }
}
