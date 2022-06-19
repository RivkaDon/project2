namespace WebAPI.Models
{
    public class ChatList
    {
        private List<Chat> chats;
        public string id { get; set; }

        public ChatList(List<Chat> chats)
        {
            this.chats = chats.ConvertAll(chat => new Chat(chat.Id, chat.ContactIdA, chat.ContactIdB, chat.Messages));
        }


        public ChatList()
        {
            this.chats = new List<Chat>();
            this.chats.Add(new Chat("1", "harry", "queen", new MessageList()));
            this.chats.Add(new Chat("2", "harry", "donald", new MessageList()));
            this.chats.Add(new Chat("3", "harry", "snow", new MessageList()));
            this.chats.Add(new Chat("4", "harry", "olof", new MessageList()));
            this.chats.Add(new Chat("5", "queen", "donald", new MessageList()));
            this.chats.Add(new Chat("6", "queen", "snow", new MessageList()));
            this.chats.Add(new Chat("7", "queen", "olof", new MessageList()));
            this.chats.Add(new Chat("8", "donald", "snow", new MessageList()));
            this.chats.Add(new Chat("9", "donald", "olof", new MessageList()));
            this.chats.Add(new Chat("10", "snow", "olof", new MessageList()));
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

        public void Edit(Chat chat, string contactIdA = null, string contactIdB = null, MessageList messageList = null)
        {
            if (chat == null) return;
            if (chats.Contains(chat))
            {
                int index = chats.IndexOf(chat);

                if (contactIdA != null) chats[index].ContactIdA= contactIdA;
                if (contactIdB != null) chats[index].ContactIdB = contactIdB;
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
