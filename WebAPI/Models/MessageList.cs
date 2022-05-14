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
    }
}
