namespace WebAPI.Models
{
    public class Chat
    {
        public string Id { get; set; }
        public Contact Contact { get; set; }
        public MessageList Messages { get; set; }


        public Chat() { }

        public Chat(string id, Contact contact, MessageList m)
        {
            this.Id = id;
            this.Contact = new Contact(contact);
            this.Messages = new MessageList(id, m);
        }

        public Chat(Chat c)
        {
            this.Id = c.Id;
            this.Contact = new Contact(c.Contact);
            this.Messages = new MessageList(c.Id, c.Messages);
        }
        
        

    }
}
