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
        private IContactService userService;
        private IChatService chatService;
        public ContactsController()
        {
            userService = new UserService();
            chatService = new ChatService();
        }

        // GET: api/<ContactsController>
        /// <summary>
        /// Returns all contacts.
        /// </summary>
        /// <returns>List of contacts</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ContactsController>/5
        /// <summary>
        /// Returns the contact with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Contact</returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContactsController>
        /// <summary>
        /// Creates new contact.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! change this</returns>
        [HttpPost]
        public IActionResult Post([FromBody] RequestCreationOfNewContact request)
        {
            userService.CreateContact(request.Id, request.Name, request.Server);
            return StatusCode(200, "hello request" + request.Id);
        }

        // PUT api/<ContactsController>/5
        /// <summary>
        /// Adding a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContactsController>/5
        /// <summary>
        /// Deletes a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
        [HttpGet]
        [Route("/api/contacts/{id}")]
        public IActionResult ReturnContact(string id)
        {
            return Ok(userService.Get(id));
        }
    }
}
