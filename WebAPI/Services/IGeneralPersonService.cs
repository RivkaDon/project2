using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IGeneralPersonService
    {
        public void CreatePerson(string name, string password);

        public Person Get(string id);

        List<Person> GetAll();

        public void Edit(string id, string name);

        public void Delete(string id);
    }
}
