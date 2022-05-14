using WebAPI.Models;

namespace WebAPI.Services
{
    public class ContactService : IContactService
    {
        private IUserService userService = new UserService();
        /*public static ContactList contacts = new ContactList();*/

        public List<Contact> GetAllContacts()
        {
            User user = userService.Get(Global.Id);
            return user.Contacts.Contacts;
        }
        public Contact Get(string id)
        {
            throw new NotImplementedException();
        }
        public void Edit(string id, string name)
        {
            throw new NotImplementedException();
        }
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public void CreateContact(string id, string name, string server)
        {
            /*contacts.Add(new Contact { Id = id, Name = name, Server = server });*/
            userService.CreateContact(id, name, server);
        }
    }
}
