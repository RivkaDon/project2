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
        private IUserService userService = new UserService();

        public ContactsController()
        {
            Global.Server = "localhost:7105";
            contactService = new ContactService(Global.Id);
            chatService = new ChatService(Global.Id);
        }

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


        private int updateChat(string action, string id1, string id2 = null, string content = null, DateTime? now = null)
        {
            chatService = new ChatService(id1);
            Chat c = chatService.Get(Global.Id);

            if (c != null)
            {
                messageService = new MessageService(c);

                switch (action)
                {
                    case "post":
                        //if (content == null) return 1;
                        messageService.SendMessage(content, true);
                        break;
                    case "put":
                        messageService.Edit(id2, true, content, now);
                        break;
                    case "delete":
                        messageService.Delete(id2);
                        break;
                }
                
                chatService = new ChatService(Global.Id);
                return 0;
            }
            if (!userService.Exists(id1)) return 0;
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
        /// <param name="id"></param>
        /// <param name="request"></param>
        [HttpPost("{id}/messages")]
        public void Post(string id, [FromBody] RequestCreationOfNewMessage request)
        {
            if (setMessageService(id) > 0)
            {
                Response.StatusCode = 404;
                return;
            }
            messageService.SendMessage(request.Content, true);
            
            if (updateChat("post", id, null, request.Content) > 0)
            {
                Response.StatusCode = 404;
            }

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
            if (contact != null)
            {
                if (contactService.Edit(id, request.Name, request.Server) > 0)
                {
                    Response.StatusCode = 404;
                    return;
                }

                if (chatService.Edit(id, contact) > 0)
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
            bool b1 = setMessageService(id1) > 0;
            bool b2 = messageService.Get(id2) == null;
            if (b1 || b2)
            {
                Response.StatusCode = 404;
                return;
            }

            DateTime now = DateTime.Now;
            messageService.Edit(id2, true, request.Content, now);

            if (updateChat("put", id1, id2, request.Content, now) > 0)
            {
                Response.StatusCode = 404;
            }

            Response.StatusCode = 204;

        }

        /// <summary>
        /// Deletes a certain contact, by id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            if (chatService.Delete(id) > 0)
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

            if (updateChat("delete", id1, id2) > 0)
            {
                Response.StatusCode = 404;
                return;
            }

            Response.StatusCode = 204;
        }
    }
}
