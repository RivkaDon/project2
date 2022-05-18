namespace WebAPI.Models
{
    public class ChatList
    {
        private List<Chat> chats = new List<Chat>();

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
