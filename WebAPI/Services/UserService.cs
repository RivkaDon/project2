﻿using WebAPI.Models;

namespace WebAPI.Services
{
    public class UserService : IUserService, IContactService
    {

        public static UserList users = new UserList();

        public List<User> GetAllUsers()
        {
            return users.Users;
        }

        public User Get(string id)
        {
            return users.Users.Find(e => e.Id == id);
        }

        public void Edit(string id, string name)
        {
            User userForEdit = Get(id);
            userForEdit.Name = name;

            
        }

        public void Delete(string id)
        {
            users.Users.Remove(Get(id));

        }

        public void CreateUser(string name, string password)
        {
            var len = users.Users.Count();
            string id = len.ToString();
            users.Add(new User(id, name, password));
        }

        public void CreateContact(string id, string name, string server)
        {
            User user = Get(id);
            if (user != null)
            {
                user.Contacts.Add(new Contact() { Id = id, Name = name, Server = server, Last = "", LastDate = null });
            }
            
        }
    }
}
