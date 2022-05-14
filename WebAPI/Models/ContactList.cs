namespace WebAPI.Models
{
    public class ContactList
    {
        private List<Contact> contacts = new List<Contact>();

        public ContactList() { }

        public List<Contact> Contacts
        {
            get { return contacts; }
        }

        public void Add(Contact contact)
        {
            contacts.Add(contact);
        }

        public void Edit(Contact contact, string name)
        {

        }
    }
}
