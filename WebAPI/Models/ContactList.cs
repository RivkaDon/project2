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
            if (contact == null) return;
            if (!contacts.Contains(contact))
            {
                contacts.Add(contact);
            }
        }

        public void Edit(Contact contact, string name = null, string last = null, DateTime? lastDate = null)
        {
            if (contact == null) return;
            if (contacts.Contains(contact))
            {
                int index = contacts.IndexOf(contact);

                if (name != null) contacts[index].Name = name;
                if (last != null) contacts[index].Last = last;
                if (lastDate != null) contacts[index].LastDate = lastDate;
            }
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
