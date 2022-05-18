using WebAPI.Models;

namespace WebAPI.Services
{
    public class ContactService : IContactService
    {
        private IUserService userService = new UserService();
        private static User user;

        public ContactService()
        {
            user = userService.Get(Global.Id);
        }

        public User User()
        {
            return user;
        }

        public List<Contact> GetAllContacts()
        {
            if (user == null) return null;
            if (user.Contacts == null) return null;
            return user.Contacts.Contacts;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;

            List<Contact> contacts = GetAllContacts();
            if (contacts == null) return false;

            foreach (var contact in contacts)
            {
                if (contact.Id == id) return true;
            }
            return false;
        }

        public Contact Get(string id)
        {
            if (!Exists(id)) return null;
            return GetAllContacts().Find(e => e.Id == id);
        }
        public void Edit(string id, string name = null, string server = null, string last = null, DateTime? lastDate = null)
        {
            if (Exists(id))
            {
                Contact contact = Get(id);
                user.Contacts.Edit(contact, name, server, last, lastDate);
            }
        }
        public void Delete(string id)
        {
            if (Exists(id))
            {
                Contact contact = Get(id);
                userService.DeleteContact(contact);
            }
        }

        public void CreateContact(string id, string name, string server)
        {
            if (Exists(id)) return;
            /*contacts.Add(new Contact { Id = id, Name = name, Server = server });*/
            userService.CreateContact(id, name, server);
        }

        public void DeleteMessage(Contact contact, Message message)
        {
            contact.Messages.Remove(message);
        }
    }
}
