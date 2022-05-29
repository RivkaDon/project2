using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private static UserList users = new UserList();
        //private User user;

        public UserService()
        {
            //users = new UserList();
        }

        /*public static void SetUsers()
        {
            users = new UserList();
        }*/

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

        public int CreateContact(string id1, string id2, string name, string server)
        {
            User user = Get(id1);
            if (user != null)
            {
                /*Contact contact = new Contact()
                {
                    Id = id2,
                    Name = name,
                    Server = server,
                    Last = null,
                    LastDate = null
                };*/
                Contact contact = new Contact(id2, name, server);
                user.Contacts.Add(contact);

                Chat chat = new Chat(
                    id2,
                    contact,
                    new MessageList()
                );

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

        public int CreateChat(string id1, string id2, string name, string server)
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
        }
    }
}
