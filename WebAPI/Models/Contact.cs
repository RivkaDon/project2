namespace WebAPI.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Last { get; set; }
        public DateTime? LastDate { get; set; }

        public Contact() { }
        public Contact(Contact c)
        {
            Id = c.Id;
            Name = c.Name;
            Server = c.Server;
            Last = c.Last;
            LastDate = c.LastDate;
        }

        public Contact(string id, string name, string server)
        {
            Id = id;
            Name = name;
            Server = server;
            Last = null;
            LastDate = null;
        }
        public Contact(string id, string name, string server, string last, DateTime? lastDate)
        {
            Id = id;
            Name = name;
            Server = server;
            Last = last;
            LastDate = lastDate;
        }
    }
}
