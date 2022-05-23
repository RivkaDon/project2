namespace WebAPI.Models
{
    public class Contact // Chat
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }

        /*public MessageList MessageListSent { get; set; }
        public MessageList MessageListReceived { get; set; }*/

        public string Last { get; set; }
        public DateTime? LastDate { get; set; }

    }
}
