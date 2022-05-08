namespace WebAPI.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Body { get; set; }
        
        public User SentBy { get; set; }

        public User SentTo { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
