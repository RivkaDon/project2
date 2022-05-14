namespace WebAPI.Models
{
    public class User
    {
        private string _id;
        private string _name;
        private string _password;
        private List<Contact> _contacts;

        public User() { } // delete

        public User(string id, string name, string password)
        {
            this._id = id; // check if this id doesn't exist!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            this._name = name; // check if valid
            this._password = password; // same
        }

        public string Id {
            get { return _id; } // maybe a copy..
        }
        public string Name {
            get { return _name; }
            set { _name = value; }
        }
        
        public string Password {
            get { return _password; }
            set { _password = value; }
        }

        public List<Contact> Contacts { get; set; }

    }
}
