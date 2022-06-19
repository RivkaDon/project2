namespace WebAPI.Models
{
    public class Message
    {
        public Message(string id, string content, DateTime? created, string sId, string rId)
        {
            Id = id;
            senderId = sId;
            receiverId = rId;
            Content = content;
            Created = created;
        }
        public Message(Message m)
        {
            Id = m.Id;
            Content = m.Content;
            Created = m.Created;
            receiverId = m.receiverId;
            senderId = m.senderId;
        }
        public Message() { }
        public string Id { get; set; }
        
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public string senderId { get; set; }
        public string receiverId { get; set; }
    }
}
