namespace WebAPI.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public bool Sent { get; set; }
    }
}
