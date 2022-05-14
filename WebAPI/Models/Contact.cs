namespace WebAPI.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public MessageList Messages { get; set; }

        /*public MessageList MessageListSent { get; set; }
        public MessageList MessageListReceived { get; set; }*/

        public string Last { get; set; }
        public DateTime? LastDate { get; set; }

    }
}
