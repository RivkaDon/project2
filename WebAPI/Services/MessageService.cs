using WebAPI.Models;

namespace WebAPI.Services
{
    public class MessageService : IMessageService
    {
        private IContactService contactService = new ContactService();
        private Contact contact;
        /*private List<Message> messages = new List<Message>();*/

        public MessageService(Contact contact)
        {
            this.contact = contact;
        }

        /// <summary>
        /// Returns if a message exists in a chat with a certain contact.
        /// </summary>
        /// <param name="id1">Contact</param>
        /// <param name="id2">Message</param>
        /// <returns>bool</returns>
        /*public bool Exists(string id1, string id2)
        {
            if (string.IsNullOrEmpty(id1) || string.IsNullOrEmpty(id2)) return false;

            Contact contact = contactService.Get(id1);
            if (contact == null) return false; // Checking if the contact exists.
            if (contact.Messages == null) return false;

            List<Message> messages = GetAllMessages(contact);
            foreach (Message message in messages)
            {
                if (message.Id == id1) return true;
            }
            return false;
        }*/
        
        public List<Message> GetAllMessages()
        {
            if (contact == null) return null;
            if (contact.Messages == null) return null;
            return contact.Messages.Messages;
        }

        public bool Exists(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;
            
            List<Message> messages = GetAllMessages();
            if (messages == null) return false;

            foreach (Message message in messages)
            {
                if (message.Id == id) return true;
            }
            return false;
        }

        public Message Get(string id)
        {
            if (!Exists(id)) return null;
            return GetAllMessages().Find(x => x.Id == id);
        }

        public void Edit(string id, bool sent, string content = null, DateTime? created = null)
        {
            if (Exists(id))
            {
                Message message = Get(id);
                contact.Messages.Edit(message, sent, content, created);
            }
        }

        public void Delete(string id)
        {
            if (Exists(id))
            {
                Message message = Get(id);
                contactService.DeleteMessage(contact, message);
            }
        }

        public void SendMessage(string content)
        {
            if (content != null)
            {
                Message message = new Message();
                int len = GetAllMessages().Count;

                message.Id = len.ToString();
                message.Created = DateTime.Now;
                message.Content = content;
                message.Sent = true;

                contact.Messages.Add(message);
            }
        }
    }
}
