namespace WebAPI.Services
{
    public interface IContactService : IGeneralUserService
    {
        public void CreateContact(string id, string name, string server);
    }
}
