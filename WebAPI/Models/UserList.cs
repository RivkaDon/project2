namespace WebAPI.Models
{
    public class UserList
    {
        private List<User> users = new List<User>();

        public UserList()
        {
            this.Add(new User("harry", "rik", "12345"));
            this.Add(new User("1", "A", "12345"));
            this.Add(new User("2", "B", "12345"));
            this.Add(new User("3", "C", "12345"));
            this.Add(new User("4", "D", "12345"));
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
}
