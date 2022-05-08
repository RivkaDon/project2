using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IGeneralUserService
    {
        public void CreateUser(string name, string password);

        public User Get(string id);

        List<User> GetAllUsers();

        public void Edit(string id, string name);

        public void Delete(string id);
    }
}
