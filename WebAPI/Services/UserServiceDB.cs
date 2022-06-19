using WebAPI.Models;
using WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Services
{
    public class UserServiceDB : IUserService
    {
        WebAPIContext _context;
        static List<User> users;

        public async Task InitializeUsers()
        {
            //_context.ChangeTracker.Clear();
            using (var db = _context)
            {
                users = await db.Users.Include(x => x.Contacts).AsNoTracking().ToListAsync();
                if (users.Count == 0)
                {
                    var userList = new UserList();
                    users = userList.Users;
                    foreach (User user in users)
                    {
                        db.Users.Add(user);
                        db.SaveChanges();

                        //_context.ChangeTracker.Clear();
                    }

                }
            }
        }
        public UserServiceDB (WebAPIContext context)
        {
            _context = context;
        }

        public bool ContactExists(string id, string contactId)
        {
            if (string.IsNullOrEmpty(id)) return false;

            List<Contact> contacts = GetAllContacts(id);
            if (contacts == null) return false;

            foreach (var contact in contacts)
            {
                if (contact.ContactId == contactId) return true;
            }
            return false;
        }

        public int CreateContact(string id, string contactId, string name, string server)
        {
            if (!Exists(id)) return 1;
            User user = Get(id);
            Contact contact = new Contact(id, contactId, name, server);
            user.Contacts.Add(contact);
            _context.Contact.Add(contact);
            _context.SaveChanges();
            return 0;
        }

        public int CreateUser(string id, string name, string password)
        {
            if (Exists(id)) return 1;

            User user = new User
            {
                Id = id,
                Name = name,
                Password = password,
                Contacts = new List<Contact>()
            };
            users.Add(user);
            _context.Users.Add(user);
            _context.SaveChanges();
            return 0;
        }

        public void Delete(string id)
        {
            if (Exists(id))
            {
                User user = Get(id);
                GetAllUsers().Remove(user);
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public int DeleteContact(string id, string contactId)
        {
            User user = Get(id);
            if (user != null)
            {
                var contact = Get(id).Contacts.FirstOrDefault(e => e.ContactId == contactId);
                if (contact != null)
                {
                    user.Contacts.Remove(contact);
                    _context.Contact.Remove(contact);
                    _context.SaveChanges();
                    return 0;
                }
            }
            return 1;
        }

        public void Edit(string id, string name = null, string password = null)
        {
            if (Exists(id))
            {
                User user = Get(id);
                if (users.Contains(user))
                {
                    int index = users.IndexOf(user);

                    if (name != null) users[index].Name = name;
                    if (password != null) users[index].Password = password;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
            }
        }

        public int EditContact(string id, string contactId, string name = null, string server = null, string last = null, DateTime? lastDate = null)
        {
            if (Get(id) == null) return 1;
            Contact contact = Get(id).Contacts.FirstOrDefault(e => e.ContactId == contactId);
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
                _context.Contact.Update(contact);
                _context.SaveChanges();
                return 0;
            }
            return 1;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;

            foreach (var user in users)
            {
                if (user.Id == id) return true;
            }
            return false;
        }

        public User Get(string id)
        {
            if (!Exists(id)) return null;
            return users.Find(e => e.Id == id);
        }

        public List<Contact> GetAllContacts(string id)
        {
            User user = Get(id);
            if (user == null) return null;
            if (user.Contacts == null) return null;
            return user.Contacts;
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public Contact GetContact(string id, string contactId)
        {
            if (!ContactExists(id, contactId)) return null;
            return GetAllContacts(id).Find(e => e.ContactId == contactId);
        }
    }
}
