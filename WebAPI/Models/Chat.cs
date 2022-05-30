namespace WebAPI.Models
{
    public class Chat
    {
        public Chat(Chat c)
        {
            this.Id = c.Id;
            this.Contact = new Contact(c.Contact);
            this.Messages = new MessageList(c.Messages);
        }
        public Chat(string id, Contact contact, MessageList m)
        {
            this.Id = id;
            this.Contact = new Contact(contact);
            this.Messages = new MessageList(m);
        }
        public string Id { get; set; }
        public Contact Contact { get; set; }
        public MessageList Messages { get; set; }

    }
}
