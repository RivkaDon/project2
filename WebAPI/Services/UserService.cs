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
            if (Exists(id)) GetAllUsers().Remove(Get(id));
        }

        public int CreateUser(string id, string name, string password)
        {
            if (Exists(id)) return 1;

            users.Add(new User {
                Id = id, Name = name, Password = password, Contacts = new ContactList(), Chats = new ChatList()
            });
            return 0;
        }

        public int CreateContact(string id, string name, string server)
        {
            if (user != null)
            {
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
            return 0;
        }

        public int updateUser(string id, Contact contact, string last, DateTime? lastDate)
        {
            User user = Get(id);
            if (user == null) return 1;
            if (contact == null) return 1;

            user.Contacts.Update(contact, last, lastDate);
            return 0;
        }

        public int CreateChat(string id, string name, string server)
        {
            if (!Exists(id)) return 1;
            return CreateContact(id, name, server);
        }

        public int CreateChatInvitation(string id, string name, string server)
        {
            return CreateContact(id, name, server);
        }

        public void DeleteContact(Contact contact)
        {
            users.DeleteContact(user, contact);
        }

        public void DeleteChat(Chat chat)
        {
            DeleteContact(chat.Contact);
            users.DeleteChat(user, chat);
        }
    }
}
