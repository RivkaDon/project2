using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User Get(string id);
        public void Edit(string id, string name = null, string password = null);
        public void Delete(string id);
        public int CreateUser(string id, string name, string password);
        public int CreateContact(string id, string name, string server);
        public int updateUser(string id, Contact contact, string last, DateTime? lastDate);
        public int CreateChat(string id, string name, string server);
        public void DeleteContact(Contact contact);
        public void DeleteChat(Chat chat);
    }
}
