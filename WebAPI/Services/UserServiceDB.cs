using WebAPI.Models;
using WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Services
{
    public class UserServiceDB : IUserService
    {
        public bool ContactExists(string id, string contactId)
        {
            throw new NotImplementedException();
        }

        public int CreateContact(string id, string contactId, string name, string server)
        {
            throw new NotImplementedException();
        }

        public int CreateUser(string id, string name, string password)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public int DeleteContact(string id, string contactId)
        {
            throw new NotImplementedException();
        }

        public void Edit(string id, string name = null, string password = null)
        {
            throw new NotImplementedException();
        }

        public int EditContact(string id, string contactId, string name = null, string server = null, string last = null, DateTime? lastDate = null)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string id)
        {
            throw new NotImplementedException();
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<Contact> GetAllContacts(string id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Contact GetContact(string id, string contactId)
        {
            throw new NotImplementedException();
        }
    }
}
