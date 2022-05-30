namespace WebAPI.Models
{
    public class Message
    {
        public Message(Message m)
        {
            Id = m.Id;
            Content = m.Content;
            Created = m.Created;
            Sent = m.Sent;
        }

        public Message()
        {
            ;
        }
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public bool Sent { get; set; }
    }
}
