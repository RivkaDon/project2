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
        public ContactsController()
        {
            contactService = new ContactService();
        }

        // GET: api/<ContactsController>
        /// <summary>
        /// Returns all contacts.
        /// </summary>
        /// <returns>List of contacts</returns>
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

        // ------> GET api/<ContactsController>/:id/messages ////////////////////////////////////////////////////// <--------
        /*[HttpGet("{id}/messages")]
        public MessageList Get(string id, string name)
        {
            //
        }*/

        // GET api/<ContactsController>/:id/messages/:id
        /// <summary>
        /// Returns a certain message sent in a chat with a certain contact.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns>Message</returns>
        [HttpGet("{id}/messages/{id}")]
        public Message Get(string id1, string id2)
        {
            return null;
            //
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
        public void Post([FromBody] string id)
        {
            //
        }

        // PUT api/<ContactsController>/:id
        /// <summary>
        /// Updates a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put([FromBody] RequestEditContact request)
        {
            contactService.Edit(request.Id, request.Name, request.Server);
        }

        // POST api/<ContactsController>/:id/messages/:id
        /// <summary>
        /// Updates a certain message sent in a chat with a certain contact.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        [HttpPut("{id}/messages/{id}")]
        public void Put([FromBody] string id1, string id2)
        {
            //
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
        [HttpDelete("{id}/messages/{id}")]
        public void Delete(string id1, string id2)
        {

        }
    }
}
