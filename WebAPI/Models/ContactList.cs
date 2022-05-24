namespace WebAPI.Models
{
    public class ContactList
    {
        private List<Contact> contacts = new List<Contact>();

        public List<Contact> Contacts
        {
            get { return contacts; }
        }

        public void Add(Contact contact)
        {
            if (contact == null) return;
            if (!contacts.Contains(contact))
            {
                contacts.Add(contact);
            }
        }

        public void Edit(Contact contact, string name = null, string server = null, string last = null, DateTime? lastDate = null)
        {
            if (contact == null) return;
            if (contacts.Contains(contact))
            {
                int index = contacts.IndexOf(contact);

                if (name != null) contacts[index].Name = name;
                if (server != null) contacts[index].Server = server;
                if (last != null) contacts[index].Last = last;
                if (lastDate != null) contacts[index].LastDate = lastDate;
            }
        }

        public void Update(Contact contact, string last, DateTime? lastDate)
        {
            Edit(contact, contact.Name, contact.Server, last, lastDate);
        }

        public void Remove(Contact contact)
        {
            if (contact == null) return;
            if (contacts.Contains(contact))
            {
                contacts.Remove(contact);
            }
        }
    }
}
