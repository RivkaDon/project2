using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IChatService chatService;
        private IUserService userService;

        public ContactsController(IUserService us, IChatService cs)
        {
            chatService = cs;
            userService = us;
        }

        /*private int updateChat(string action, string id1, string id2 = null, string content = null, DateTime? now = null)
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            Chat c = chatService.Get(id1, Global.Id); // Global.id

            if (c != null)
            {
                //messageService = new MessageService(c);

                switch (action)
                {
                    case "post":
                        //if (content == null) return 1;
                        messageService.SendMessage(Global.Id, id1, content, true);
                        break;
                    case "put":
                        messageService.Edit(id2, true, content, now);
                        break;
                    case "delete":
                        messageService.Delete(id2);
                        break;
                }
                
                //chatService = new ChatService(); // Global.Id
                return 0;
            }
            if (!userService.Exists(id1)) return 0;
            return 1;
        }*/

        /// <summary>
        /// Returns all contacts.
        /// </summary>
        /// <returns>List<Contact></returns>
        [HttpGet]
        public List<Contact> Get()
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            Response.StatusCode = 200;
            return userService.GetAllContacts(Global.Id);
        }

        /// <summary>
        /// Returns the contact with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Contact</returns>
        [HttpGet("{id}")]
        public Contact Get(string id)
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            Contact contact = userService.GetContact(Global.Id, id);
            if (contact == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return contact;
        }

        /// <summary>
        /// Returns all messages.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<Message></returns>
        [HttpGet("{id}/messages")]
        public List<Message> GetMessages(string id)
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            /*if (setMessageService(Global.Id, id) > 0)
            {
                Response.StatusCode = 404;
                return null;
            }*/
            List<Message> messages = chatService.GetMessageBetween(Global.Id, id);

            if (messages == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return messages;
        }

        /// <summary>
        /// Returns a certain message sent in a chat with a certain contact.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns>Message</returns>
        [HttpGet("{id1}/messages/{id2}")]
        public Message Get(string id1, string id2)
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            /*if (setMessageService(Global.Id, id1) > 0)
            {
                Response.StatusCode = 404;
                return null;
            }*/
            Message message = chatService.GetMessageById(Global.Id, id1, id2);

            if (message == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Response.StatusCode = 200;
            return message;
        }

        /// <summary>
        /// Creates new contact.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] RequestCreationOfNewContact request)
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            // setChatService(Global.Id);
            int num = userService.CreateContact(Global.Id, request.Id, request.Name, request.Server);
            if (num > 0)
            {
                Response.StatusCode = 404;
                return;
            }
            num = chatService.CreateChat(Global.Id, request.Id, request.Name, request.Server);
            if (num > 0)
            {
                Response.StatusCode = 404;
                return;
            }
            Response.StatusCode = 201;
        }

        /// <summary>
        /// Creates new message.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        [HttpPost("{id}/messages")]
        public void Post(string id, [FromBody] RequestCreationOfNewMessage request)
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            /*if (setMessageService(Global.Id, id) > 0)
            {
                Response.StatusCode = 404;
                return;
            }*/

            if (chatService.CreateMessage(Global.Id, id, request.Content) == 0)
            {
                Response.StatusCode = 404;
                return;
            }
            //messageService.SendMessage(id, Global.Id, request.Content, false);

            /*if (updateChat("post", id, null, request.Content) > 0)
            {
                Response.StatusCode = 404;
            }*/

            Response.StatusCode = 201;
        }

        /// <summary>
        /// Updates a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] RequestEditContact request)
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            Contact contact = userService.GetContact(Global.Id, id);
            if (contact != null)
            {
                if (userService.EditContact(Global.Id, id, request.Name, request.Server, null) > 0)
                {
                    Response.StatusCode = 404;
                    return;
                }
                Response.StatusCode = 204;
                return;
            }
            Response.StatusCode = 404;
        }

        /// <summary>
        /// Updates a certain message sent in a chat with a certain contact.
        /// </summary>
        /// <param name="id1">Contact</param>
        /// <param name="id2">Message</param>
        /// <param name="request"></param>
        [HttpPut("{id1}/messages/{id2}")]
        public void Put(string id1, string id2, [FromBody] RequestEditMessage request)
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            //bool b1 = setMessageService(Global.Id, id1) > 0;
            //bool b2 = messageService.Get(id1, id2) == null;
            if (chatService.GetMessageById(Global.Id, id1, id2) == null)
            {
                Response.StatusCode = 404;
                return;
            }

            DateTime now = DateTime.Now;
            chatService.EditMessageById(Global.Id, id1, id2,request.Content);
            Response.StatusCode = 204;
        }

        /// <summary>
        /// Deletes a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            if (userService.DeleteContact(Global.Id, id) > 0)
            {
                Response.StatusCode = 404;
                return;
            }
            Response.StatusCode = 204;
        }

        /// <summary>
        /// Deletes a certain message sent in a chat with a certain contact.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        [HttpDelete("{id1}/messages/{id2}")]
        public void Delete(string id1, string id2)
        {
            Global.Id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            if (chatService.Get(Global.Id, id1) == null)
            {
                Response.StatusCode = 404;
                return;
            }
            if (chatService.DeleteMessage(Global.Id, id1, id2) > 0)
            {
                Response.StatusCode = 404;
                return;
            }
            Response.StatusCode = 204;
        }
    }
}
