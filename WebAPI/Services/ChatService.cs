using WebAPI.Models;

namespace WebAPI.Services
{
    public class ChatService : IChatService
    {

        public static List<User> users = new List<User>();

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
    }
}
