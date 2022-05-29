namespace WebAPI.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Last { get; set; }
        public DateTime? LastDate { get; set; }

        public Contact(Contact c)
        {
            Id = c.Id;
            Name = c.Name;
            Server = c.Server;
            Last = c.Last;
            LastDate = c.LastDate;
        }

        public Contact(string Id1, string Name1, string Server1)
        {
            Id = Id1;
            Name = Name1;
            Server = Server1;
            Last = null;
            LastDate = null;
        }
    }
}
