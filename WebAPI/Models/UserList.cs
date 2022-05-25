namespace WebAPI.Models
{
    public class UserList
    {
        private List<User> users = new List<User>();

        public UserList()
        {
            this.Add(new User { Id = "harry", Name = "rik", Password = "12345", Contacts = new ContactList(), Chats = new ChatList() });
            this.Add(new User { Id = "1", Name = "A", Password = "12345", Contacts = new ContactList(), Chats = new ChatList() });
            this.Add(new User { Id = "2", Name = "B", Password = "12345", Contacts = new ContactList(), Chats = new ChatList() });
            this.Add(new User { Id = "3", Name = "C", Password = "12345", Contacts = new ContactList(), Chats = new ChatList() });
            this.Add(new User { Id = "4", Name = "D", Password = "12345", Contacts = new ContactList(), Chats = new ChatList() });
        }

        public List<User> Users
        {
            get { return users; }
        }

        public ContactList GetContactList(User user)
        {
            if (user == null) return null;
            if (!users.Contains(user)) return null;
            return user.Contacts;
        }

        public ChatList GetChatList(User user)
        {
            if (user == null) return null;
            if (!users.Contains(user)) return null;
            return user.Chats;
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
            if (contact == null) return;

            ContactList contactList = GetContactList(user);
            if (contactList == null) return;
            if (!contactList.Contacts.Contains(contact)) return;

            contactList.Remove(contact);
        }

        public void DeleteChat(User user, Chat chat)
        {
            if (chat == null) return;

            ChatList chatList = GetChatList(user);
            if (chatList == null) return;
            if (!chatList.Chats.Contains(chat)) return;

            chatList.Remove(chat);
        }
    }

}

