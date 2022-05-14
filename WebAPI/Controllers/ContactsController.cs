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
        private IChatService chatService;
        public ContactsController()
        {
            contactService = new ContactService();
            chatService = new ChatService();
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

        // GET api/<ContactsController>/{id}
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

        // PUT api/<ContactsController>/{id}
        /// <summary>
        /// Adding a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {

        }

        // DELETE api/<ContactsController>/{id}
        /// <summary>
        /// Deletes a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(str id)
        {
        }
    }
}
