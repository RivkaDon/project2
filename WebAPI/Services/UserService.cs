using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private static UserList users = new UserList();
        private User user;

        public UserService()
        {
            user = Get(Global.Id);
        }

        public UserService(string id)
        {
            user = Get(id);
        }

        public List<User> GetAllUsers()
        {
            return users.Users;
        }

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
            if (Exists(id))
            {
                GetAllUsers().Remove(Get(id));
            }
        }

        public void CreateUser(string id, string name, string password)
        {
            if (Exists(id)) return;
            /*var len = GetAllUsers().Count();
            string id = len.ToString();*/
            users.Add(new User {
                Id = id, Name = name, Password = password, Contacts = new ContactList(), Chats = new ChatList()
            });
        }

        public void CreateContact(string id, string name, string server)
        {
            if (!Exists(id)) return; // ///////////////////////////////////////
            // User user = Get(id);
            if (user != null)
            {
                /*if (user.Contacts == null)
                {
                    user.Contacts = new ContactList();
                }*/
                Contact contact = new Contact()
                {
                    Id = id,
                    Name = name,
                    Server = server,
                    Last = null,
                    LastDate = null
                };
                user.Contacts.Add(contact);

                Chat chat = new Chat()
                {
                    Id = id,
                    Contact = contact,
                    Messages = new MessageList()
                };

                user.Chats.Add(chat);
            }
        }

        public void updateUser(string id, Contact contact, string last, DateTime? lastDate)
        {
            User user = Get(id);
            if (user == null) return;
            if (contact == null) return;

            user.Contacts.Update(contact, last, lastDate);
        }

        public void CreateChat(string id, string name, string server)
        {
            CreateContact(id, name, server);
        }

        public void DeleteContact(Contact contact)
        {
            // User user = Get(id);
            users.DeleteContact(user, contact);
        }

        public void DeleteChat(Chat chat)
        {
            // User user = Get(id);
            DeleteContact(chat.Contact);
            users.DeleteChat(user, chat);
        }
    }
}
