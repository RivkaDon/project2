namespace WebAPI.Models
{
    public class UserList
    {
        private List<User> users = new List<User>();

        public UserList()
        {
            this.Add(new User { Id = "harry", Name = "rik", Password = "12345", Contacts = null });
            this.Add(new User { Id = "1", Name = "A", Password = "12345", Contacts = null });
            this.Add(new User { Id = "2", Name = "B", Password = "12345", Contacts = null });
            this.Add(new User { Id = "3", Name = "C", Password = "12345", Contacts = null });
            this.Add(new User { Id = "4", Name = "D", Password = "12345", Contacts = null });
        }

        public List<User> Users
        {
            get { return users; }
        }

        public void Add(User user)
        {
            users.Add(user);
        }

        public void Edit(User user, string name)
        {
            
        }
    }

}

