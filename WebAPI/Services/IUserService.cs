using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User Get(string id);
        public void Edit(string id, string name = null, string password = null);
        public void Delete(string id);
        public void CreateUser(string id, string name, string password);
        public void CreateContact(string id, string name, string server);
        public void CreateChat(string id, string name, string server);
        public void DeleteContact(Contact contact);
        public void DeleteChat(Chat chat);
    }
}
