using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IContactService
    {
        public List<Contact> GetAllContacts();
        public Contact Get(string id);
        public void Edit(string id, string name);
        public void Delete(string id);
        public void CreateContact(string id, string name, string server);
    }
}
