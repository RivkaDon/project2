using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI;
using WebAPI.Services;

namespace WebAPI.Models
{
    public class Contact
    {
        
        [Key, Column(Order = 1)]
        public string ContactId { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Last { get; set; }
        public DateTime? LastDate { get; set; }
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        public Contact() { }
        public Contact(Contact c)
        {
            ContactId = c.ContactId;
            Name = c.Name;
            Server = c.Server;
            Last = c.Last;
            LastDate = c.LastDate;
            UserId = c.UserId;
        }

        public Contact(string userid, string contactId, string name, string server)
        {
            ContactId = contactId;
            Name = name;
            Server = server;
            Last = null;
            LastDate = null;
            UserId = userid;
            //UserService us = new UserService();
            //User = us.Get(userid);
        }
        public Contact(string userid, string contactId, string name, string server, string last, DateTime? lastDate)
        {
            ContactId= contactId;
            Name = name;
            Server = server;
            Last = last;
            LastDate = lastDate;
            UserId = userid;
            //UserService us = new UserService();
            //User = us.Get(userid);
        }
    }
}
