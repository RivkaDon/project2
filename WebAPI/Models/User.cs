namespace WebAPI.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ContactList Contacts { get; set; }
        public ChatList Chats { get; set; }
    }
}
