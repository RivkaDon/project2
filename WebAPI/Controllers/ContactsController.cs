using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ContactsController : ControllerBase
    {
        private IContactService contactService;
        private IChatService chatService;
        private IMessageService messageService;

        public ContactsController()
        {
            Global.Id = "1"; // delete later!
            contactService = new ContactService(Global.Id);
            chatService = new ChatService(Global.Id);
        }

        /*private void setChatService(string id)
        {
            chatService = new ChatService(id);
        }*/

        private int setMessageService(string id)
        {
            Chat c = chatService.Get(id);
            if (c != null)
            {
                messageService = new MessageService(c);
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// Returns all contacts.
        /// </summary>
        /// <returns>List<Contact></returns>
        [HttpGet]
        public List<Contact> Get()
        {
            Response.StatusCode = 200;
            return contactService.GetAllContacts();
        }

        /// <summary>
        /// Returns the contact with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Contact</returns>
        [HttpGet("{id}")]
        public Contact Get(string id)
        {
            Contact contact = contactService.Get(id);
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
            if (setMessageService(id) > 0)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Message> messages = messageService.GetAllMessages();

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
            if (setMessageService(id1) > 0)
            {
                Response.StatusCode = 404;
                return null;
            }
            Message message = messageService.Get(id2);

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
            // setChatService(Global.Id);
            int num = chatService.CreateChat(request.Id, request.Name, request.Server);
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
        /// <param name="value"></param>
        [HttpPost("{id}/messages")]
        public void Post(string id, [FromBody] RequestCreationOfNewMessage request)
        {
            if (setMessageService(id) > 0)
            {
                Response.StatusCode = 404;
                return;
            }
            messageService.SendMessage(request.Content, true);
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
            Contact contact = contactService.Get(id);
            if (contact == null)
            {
                Response.StatusCode = 404;
                return;
            }

            contactService.Edit(id, request.Name, request.Server);
            
            // setChatService(Global.Id);
            chatService.Edit(id, contact);
            Response.StatusCode = 204;
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
            bool b1 = setMessageService(id1) > 0;
            bool b2 = messageService.Get(id2) == null;
            if (b1 || b2)
            {
                Response.StatusCode = 404;
                return;
            }
            messageService.Edit(id2, true, request.Content, DateTime.Now);
            Response.StatusCode = 204;

        }

        /// <summary>
        /// Deletes a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            if (chatService.Get(id) == null)
            {
                Response.StatusCode = 404;
                return;
            }
            chatService.Delete(id);
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
            bool b1 = setMessageService(id1) > 0;
            bool b2 = messageService.Get(id2) == null;
            if (b1 || b2)
            {
                Response.StatusCode = 404;
                return;
            }
            
            messageService.Delete(id2);

            List<Message> messages = messageService.GetAllMessages();
            contactService.UpdateLastDate(id1, messages);

            Response.StatusCode = 204;
        }
    }
}
