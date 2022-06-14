namespace WebAPI.Models
{
    public class ChatList
    {
        public string Id { get; set; }
        private List<Chat> chats;

        public ChatList(string id)
        {
            this.Id = id;
            this.chats = new List<Chat>();
        }

        public ChatList(string id, List<Chat> chats)
        {
            this.Id = id;
            this.chats = chats.ConvertAll(chat => new Chat(chat.Id, chat.Contact, chat.Messages));
        }


        public ChatList()
        {
            this.chats = new List<Chat>();
        }

        public List<Chat> Chats
        {
            get { return chats; }
        }

        public void Add(Chat chat)
        {
            if (chat == null) return;
            if (!chats.Contains(chat))
            {
                chats.Add(chat);
            }
        }

        public void Edit(Chat chat, Contact contact = null, MessageList messageList = null)
        {
            if (chat == null) return;
            if (chats.Contains(chat))
            {
                int index = chats.IndexOf(chat);

                if (contact != null) chats[index].Contact = contact;
                if (messageList != null) chats[index].Messages = messageList;
            }
        }

        public void Remove(Chat chat)
        {
            if (chat == null) return;
            if (chats.Contains(chat))
            {
                chats.Remove(chat);
            }
        }
    }
}
