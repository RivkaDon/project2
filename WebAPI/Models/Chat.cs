namespace WebAPI.Models
{
    public class Chat
    {
        public string Id { get; set; }
        /*public string ContactId { get; set; }*/
        public Contact Contact { get; set; }
        public MessageList Messages { get; set; }
    }
}
