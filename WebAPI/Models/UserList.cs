namespace WebAPI.Models
{
    public class UserList
    {
        private List<User> users = new List<User>();

        public UserList()
        {
            this.Add(new User { Id = "harry", Name = "rik", Password = "12345", Contacts = new ContactList() });
            this.Add(new User { Id = "1", Name = "A", Password = "12345", Contacts = new ContactList() });
            this.Add(new User { Id = "2", Name = "B", Password = "12345", Contacts = new ContactList() });
            this.Add(new User { Id = "3", Name = "C", Password = "12345", Contacts = new ContactList() });
            this.Add(new User { Id = "4", Name = "D", Password = "12345", Contacts = new ContactList() });
        }

        public List<User> Users
        {
            get { return users; }
        }

        public ContactList GetContactList(User user)
        {
            return user.Contacts;
        }

        public void Add(User user)
        {
            if (user == null) return;
            if (!users.Contains(user))
            {
                users.Add(user);
            }
        }

        public void Edit(User user, string name = null, string password = null)
        {
            if (user == null) return;
            if (users.Contains(user))
            {
                int index = users.IndexOf(user);

                if (name != null) users[index].Name = name;
                if (password != null) users[index].Password = password;
            }
        }

        public void DeleteContact(User user, Contact contact)
        {
            if (user == null || contact == null) return;
            if (!users.Contains(user)) return;

            ContactList contactList = GetContactList(user);
            if (contactList == null) return;
            if (!contactList.Contacts.Contains(contact)) return;

            contactList.Remove(contact);
        }
    }

}

