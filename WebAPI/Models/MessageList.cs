namespace WebAPI.Models
{
    public class MessageList
    {
        private List<Message> messages = new List<Message>();

        public MessageList() { }

        public List<Message> Messages
        {
            get { return messages; }
        }

        public void Add(Message message)
        {
            if (messages == null) return;
            messages.Add(message);
        }

        public void Edit(Message message, bool sent, string content = null, DateTime? created = null)
        {
            if (message == null) return;
            if (messages.Contains(message))
            {
                int index = messages.IndexOf(message);

                if (content != null) messages[index].Content = content;
                if (created != null) messages[index].Created = created;
                messages[index].Sent = sent;
            }
        }

        public void Remove(Message message)
        {
            if (messages == null) return;
            if (messages.Contains(message))
            {
                messages.Remove(message);
            }
        }
    }
}
