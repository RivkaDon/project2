using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService : IUserService, IContactService
    {

        public static List<User> users = new List<User>() {
        new User{Id = "harry", Name = "rik", Contacts = new List<Contact>() } };

        public List<User> GetAllUsers()
        {
            return users;
        }

        public User Get(string id)
        {
            return users.Find(e => e.Id == id);
        }

        public void Edit(string id, string name)
        {
            User userForEdit = Get(id);
            userForEdit.Name = name;
        }

        public void Delete(string id)
        {
            users.Remove(Get(id));

        }

        public void CreateUser(string name, string password)
        {
            users.Add(new User() { Name = name, Password = password });
        }

        public void CreateContact(string id, string name, string server)
        {
            User user = Get(Global.UserName);
            user.Contacts.Add(new Contact() {Id = id, Name = name, Server = server, Last = "", LastDate=null});
        }
    }
}
