using WebAPI.Models;

namespace WebAPI.Services
{
    public class ContactService : IContactService
    {
        private IUserService userService;
        private User user;

        public ContactService()
        {
            userService = new UserService();
            user = userService.Get(Global.Id);
        }

        public ContactService(string id)
        {
            userService = new UserService(id);
            user = userService.Get(id);
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
            Contact contact = Get(id);
            if (contact != null)
            {
                user.Contacts.Edit(contact, name, server, last, lastDate);
            }
        }
        public void Delete(string id)
        {
            Contact contact = Get(id);
            if (contact != null)
            {
                userService.DeleteContact(contact);  // add else return 1 for 404

                IUserService us = new UserService(id);
                User u = us.Get(id);
                List<Contact> contacts = u.Contacts.Contacts;
                Contact c = contacts.Find(c => c.Id == user.Id);
                us.DeleteContact(c);
            }
        }

        public void CreateContact(string id, string name, string server)
        {
            if (Exists(id)) return;
            int num = userService.CreateContact(id, name, server);

            if (num > 0) return; // return num

            IUserService us = new UserService(id);
            us.CreateContact(user.Id, user.Name, Global.Server);
        }

        public void UpdateLastDate(string id, List<Message> messages)
        {
            Contact contact = Get(id);

            if (messages.Count > 0)
            {
                DateTime? created = messages[0].Created;
                string last = messages[0].Content;

                foreach (Message m in messages)
                {
                    if (created.Value < m.Created.Value)
                    {
                        created = m.Created.Value;
                        last = m.Content;
                    }
                }

                contact.Last = last;
                contact.LastDate = created;
            } else
            {
                contact.Last = null;
                contact.LastDate = null;
            }
        }
    }
}
