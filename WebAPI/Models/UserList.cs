namespace WebAPI.Models
{
    public class UserList
    {
        private List<User> users = new List<User>();
        public UserList(List<User> usersList)
        {
            foreach (User user in usersList)
            {
                users.Add(user);
            }
        }

        public UserList()
        {
            ContactList contacts = CreateContacts();
            ChatList chats = CreateChats(contacts);

            this.Add(new User { Id = "harry", Name = "Harry Potter", Password = "12345", Contacts = GetContactList(contacts, "harry"), Chats = GetChatList(chats, "harry") });
            this.Add(new User { Id = "queen", Name = "Queen Elisabeth", Password = "12345", Contacts = GetContactList(contacts, "queen"), Chats = GetChatList(chats, "queen") });
            this.Add(new User { Id = "donald", Name = "Donald Trump", Password = "12345", Contacts = GetContactList(contacts, "donald"), Chats = GetChatList(chats, "donald") });
            this.Add(new User { Id = "snow", Name = "Snow white", Password = "12345", Contacts = GetContactList(contacts, "snow"), Chats = GetChatList(chats, "snow") });
            this.Add(new User { Id = "olof", Name = "Olof snow man", Password = "12345", Contacts = GetContactList(contacts, "olof"), Chats = GetChatList(chats, "olof") });
            this.Add(new User { Id = "1", Name = "A", Password = "12345", Contacts = new ContactList(), Chats = new ChatList() });
            this.Add(new User { Id = "2", Name = "B", Password = "12345", Contacts = new ContactList(), Chats = new ChatList() });
            this.Add(new User { Id = "3", Name = "C", Password = "12345", Contacts = new ContactList(), Chats = new ChatList() });
            this.Add(new User { Id = "4", Name = "D", Password = "12345", Contacts = new ContactList(), Chats = new ChatList() });

            //CreateChats();
        }

        private ContactList CreateContacts()
        {
            ContactList contactList = new ContactList();

            contactList.Add(new Contact("harry", "Harry Potter",  Global.Server ));
            contactList.Add(new Contact("queen", "Queen Elisabeth",  Global.Server ));
            contactList.Add(new Contact("donald", "Donald Trump", Global.Server ));
            contactList.Add(new Contact("snow", "Snow white",Global.Server ));
            contactList.Add(new Contact("olof", "Olof snow man", Global.Server ));

            return contactList;
        }

        /// <summary>
        /// Retrun a contact list that *doesn't* hold the contact with the given id.
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private ContactList GetContactList(ContactList contacts, string id)
        {
            ContactList contactList = new ContactList(id);
            foreach (Contact contact in contacts.Contacts)
            {
                if (contact.Id != id) contactList.Add(new Contact(contact));
            }
            return contactList;
        }

        private ChatList CreateChats(ContactList contacts)
        {
            ChatList chatList = new ChatList();
            foreach (Contact contact in contacts.Contacts)
            {
                chatList.Add(new Chat (contact.Id, contact, new MessageList() ));
            }
            return chatList;
        }

        /// <summary>
        /// Retrun a chat list that *doesn't* hold the chat with the given id.
        /// </summary>
        /// <param name="chats"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private ChatList GetChatList(ChatList chats, string id)
        {
            ChatList chatList = new ChatList(id);
            foreach (Chat chat in chats.Chats)
            {
                if (chat.Id != id) chatList.Add(new Chat(chat));
            }
            return chatList;
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

