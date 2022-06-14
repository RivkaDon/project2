using WebAPI.Models;
using WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Services
{
    public class UserServiceDB : IUserService
    {
        private static WebAPIContext _context;
        private static UserList users;

        //private User user;

        public UserServiceDB(WebAPIContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            UserList userList = new UserList();
            users = userList;
            /*var list = _context.User.ToList();
            if (list.Count == 0)
            {
                UserList userList = new UserList();
                //users = userList;
                foreach(var user in userList.Users)
                {
                    _context.Add(user);
                }
                _context.SaveChanges();
            } else
            {
                users = new UserList(list);
            }*/
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
        // TODO
        public void Edit(string id, string name = null, string password = null)
        {
            if (Exists(id))
            {
                _context.Update(Get(id));
                _context.SaveChanges();
                users = new UserList(_context.User.ToList());
            }
        }

        public void Delete(string id)
        {
            if (Exists(id))
            {
                _context.Remove(Get(id));
                _context.SaveChanges();
                users = new UserList(_context.User.ToList());
            }
        }

        public int CreateUser(string id, string name, string password)
        {
            if (Exists(id)) return 1;

            User user = new User
            {
                Id = id,
                Name = name,
                Password = password,
                Contacts = new ContactList(),
                Chats = new ChatList()
            };
            _context.Add(user);
            _context.SaveChanges();
            users = new UserList(_context.User.ToList());

            return 0;
        }

           // TODO
        public int CreateContact(string id1, string id2, string name, string server)
        {
            User user = _context.User.Where(x => x.Id == id1).FirstOrDefault();
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
