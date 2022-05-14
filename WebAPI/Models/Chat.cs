namespace WebAPI.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MessageList Messages { get; set; }
    }
}
