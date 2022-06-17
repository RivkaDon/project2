using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private static UserList users = new UserList();
        //private User user;

        public UserService()
        {
            Global.Server = "localhost:7105";
        }


        public List<User> GetAllUsers()
        {
            return users.Users;
        }

        /*
         * Contact section
         */

        public List<Contact> GetAllContacts(string id)
        {
            User user = Get(id);
            if (user == null) return null;
            if (user.Contacts == null) return null;
            return user.Contacts;
        }

        public bool ContactExists(string id, string contactId)
        {
            if (string.IsNullOrEmpty(id)) return false;

            List<Contact> contacts = GetAllContacts(id);
            if (contacts == null) return false;

            foreach (var contact in contacts)
            {
                if (contact.Id == contactId) return true;
            }
            return false;
        }

        public Contact GetContact(string id, string contactId)
        {
            if (!ContactExists(id, contactId)) return null;
            return GetAllContacts(id).Find(e => e.Id == contactId);
        }

        public int EditContact(string id, string contactId, string name = null, string server = null, string last = null, DateTime? lastDate = null)
        {
            if (Get(id) == null) return 1;
            Contact contact = Get(id).Contacts.FirstOrDefault(e => e.Id == contactId);
            if (contact != null)
            {
                if (name != null)
                {
                    contact.Name = name;
                }
                if (server != null)
                {
                    contact.Server = server;
                }
                if (last != null)
                {
                    contact.Last = last;
                }
                if (lastDate != null)
                {
                    contact.LastDate = lastDate;
                }
                return 0;
            }
            return 1;
        }


        public int DeleteContact(string id, string contactId)
        {
            User user = Get(id);
            if (user != null)
            {
                var contact = Get(id).Contacts.FirstOrDefault(e => e.Id == contactId);
                if (contact != null)
                {
                    user.Contacts.Remove(contact);
                    return 0;
                }
            }
            return 1;
        }

        public int CreateContact(string id, string contactId, string name, string server)
        {
            if (!Exists(id)) return 1;
            User user = Get(id);
            user.Contacts.Add(new Contact(contactId, name, server));
            return 0;
        }



        /*
         * Finish contact
         */



        public bool Exists(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;

            foreach (var user in users.Users)
            {
                if (user.Id == id) return true;
            }
            return false;
        }

        public User Get(string id)
        {
            if (!Exists(id)) return null;
            return GetAllUsers().Find(e => e.Id == id);
        }

        public void Edit(string id, string name = null, string password = null)
        {
            if (Exists(id))
            {
                User user = Get(id);
                users.Edit(user, name, password);
            }
        }

        

        
        public void Delete(string id)
        {
            if (Exists(id)) GetAllUsers().Remove(Get(id));
        }

        public int CreateUser(string id, string name, string password)
        {
            if (Exists(id)) return 1;

            users.Add(new User {
                Id = id, Name = name, Password = password, Contacts = new List<Contact>()});
            return 0;
        }

        

        /*public int updateUser(string id, Contact contact, string last, DateTime? lastDate)
        {
            User user = Get(id);
            if (user == null) return 1;
            if (contact == null) return 1;

            user.Contacts.Update(contact, last, lastDate);
            return 0;
        }*/

        /*public int CreateChat(string id1, string id2, string name, string server)
        {
            if (!Exists(id1)) return 1;
            return CreateContact(id1, id2, name, server);
        }

        public int CreateChatInvitation(string id1, string id2, string name, string server)
        {
            return CreateContact(id1, id2, name, server);
        }

        public void DeleteContact(string id, Contact contact)
        {
            User user = Get(id);
            users.DeleteContact(user, contact);
        }

        public void DeleteChat(string id, Chat chat)
        {
            User user = Get(id);
            DeleteContact(id, chat.Contact);
            users.DeleteChat(user, chat);
        }*/
    }
}
