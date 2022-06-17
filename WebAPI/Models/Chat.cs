namespace WebAPI.Models
{
    public class Chat
    {
        public string Id { get; set; }
        public string ContactIdA { get; set; }
        public string ContactIdB { get; set; }
        public MessageList Messages { get; set; }


        public Chat() { }

        public Chat(string id, string contactIdA, string contactIdB, MessageList m)
        {
            this.Id = id;
            this.ContactIdA = contactIdA;
            this.ContactIdB = contactIdB;
            this.Messages = new MessageList(id, m);
        }

        public Chat(Chat c)
        {
            this.Id = c.Id;
            this.ContactIdA = c.ContactIdA;
            this.ContactIdB = c.ContactIdB;
            this.Messages = new MessageList(c.Id, c.Messages);
        }
        
        

    }
}
