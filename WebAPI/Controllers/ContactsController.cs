using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
            contactService = new ContactService();
            chatService = new ChatService();
        }

        /*private void setChatService(string id)
        {
            chatService = new ChatService(id);
        }*/

        private void setMessageService(string id)
        {
            Chat c = chatService.Get(id);
            messageService = new MessageService(c);
        }

        /// <summary>
        /// Returns all contacts.
        /// </summary>
        /// <returns>List<Contact></returns>
        [HttpGet]
        public List<Contact> Get()
        {
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
                // Response.StatusCode = 404;
                Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
                //return new HttpNotFoundResult();
            }
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
            setMessageService(id);
            return messageService.GetAllMessages();
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
            setMessageService(id1);
            return messageService.Get(id2);
        }

        /// <summary>
        /// Creates new contact.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void Post([FromBody] RequestCreationOfNewContact request)
        {
            // setChatService(Global.Id);
            chatService.CreateChat(request.Id, request.Name, request.Server);
        }

        /// <summary>
        /// Creates new message.
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("{id}/messages")]
        public void Post(string id, [FromBody] RequestCreationOfNewMessage request)
        {
            setMessageService(id);
            messageService.SendMessage(request.Content, true);
        }

        /// <summary>
        /// Updates a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] RequestEditContact request)
        {
            contactService.Edit(id, request.Name, request.Server);
            Contact contact = contactService.Get(id);
            // setChatService(Global.Id);
            chatService.Edit(id, contact);
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
            setMessageService(id1);
            messageService.Edit(id2, true, request.Content, DateTime.Now);
            
        }

        /// <summary>
        /// Deletes a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            // setChatService(Global.Id);
            chatService.Delete(id);
        }

        /// <summary>
        /// Deletes a certain message sent in a chat with a certain contact.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        [HttpDelete("{id1}/messages/{id2}")]
        public void Delete(string id1, string id2)
        {
            setMessageService(id1);
            /*Chat c = chatService.Get(id1);
            messageService = new MessageService(c);*/
            messageService.Delete(id2);

            List<Message> messages = messageService.GetAllMessages();
            if (messages.Count > 0)
            {
                /*Contact contact = contactService.Get(id1);*/
                /*Contact contact = c.Contact;*/
                contactService.UpdateLastDate(id1, messages);

            }
        }
    }
}
