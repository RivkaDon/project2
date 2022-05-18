using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IContactService contactService;
        private IMessageService messageService;

        public ContactsController()
        {
            contactService = new ContactService();
        }

        private void setMessageService(string id)
        {
            Contact c = contactService.Get(id);
            messageService = new MessageService(c);
        }

        // GET: api/<ContactsController>
        /// <summary>
        /// Returns all contacts.
        /// </summary>
        /// <returns>List<Contact></returns>
        [HttpGet]
        public List<Contact> Get()
        {
            return contactService.GetAllContacts();
        }

        // GET api/<ContactsController>/:id
        /// <summary>
        /// Returns the contact with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Contact</returns>
        [HttpGet("{id}")]
        public Contact Get(string id)
        {
            return contactService.Get(id);
        }

        // GET api/<ContactsController>/:id/messages
        /// <summary>
        /// Returns all messages.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Message></returns>
        [HttpGet("{id}/messages")]
        public List<Message> GetMessages(string id)
        {
            setMessageService(id);
            return messageService.GetAllMessages();
        }

        // GET api/<ContactsController>/:id/messages/:id
        /// <summary>
        /// Returns a certain message sent in a chat with a certain contact.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns>Message</returns>
        [HttpGet("{id1}/messages/{id2}")]
        public Message Get(string id1, string id2)
        {
            setMessageService(id1);
            return messageService.Get(id2);
        }

        // POST api/<ContactsController>
        /// <summary>
        /// Creates new contact.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] RequestCreationOfNewContact request)
        {
            contactService.CreateContact(request.Id, request.Name, request.Server);
        }

        // POST api/<ContactsController>/:id/messages
        /// <summary>
        /// Creates new message.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("{id}/messages")]
        public void Post(string id, [FromBody] RequestCreationOfNewMessage request)
        {
            setMessageService(id);
            messageService.SendMessage(request.Content);
        }

        // PUT api/<ContactsController>/:id
        /// <summary>
        /// Updates a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] RequestEditContact request)
        {
            contactService.Edit(id, request.Name, request.Server);
        }

        // POST api/<ContactsController>/:id/messages/:id
        /// <summary>
        /// Updates a certain message sent in a chat with a certain contact.
        /// </summary>
        /// <param name="id1">Contact</param>
        /// <param name="id2">Message</param>
        /// <param name="request"></param>
        [HttpPut("{id1}/messages/{id2}")]
        public void Put(string id1, string id2, [FromBody] RequestEditMessage request)
        {
            setMessageService(id1);
            messageService.Edit(id2, request.Sent, request.Content, request.Created);
        }

        // DELETE api/<ContactsController>/:id
        /// <summary>
        /// Deletes a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            contactService.Delete(id);
        }

        // DELETE api/<ContactsController>/:id/messages/:id
        /// <summary>
        /// Deletes a certain message sent in a chat with a certain contact.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        [HttpDelete("{id1}/messages/{id2}")]
        public void Delete(string id1, string id2)
        {
            setMessageService(id1);
            messageService.Delete(id2);
        }
    }
}
