﻿using WebAPI.Models;

namespace WebAPI.Services
{
    public class MessageService : IMessageService
    {
        private IUserService userService;
        private IChatService chatService;
        private Chat chat;

        public MessageService(Chat c)
        {
            userService = new UserService();
            chatService = new ChatService(c.Id);
            chat = c;
        }

        public MessageService(Chat c, string id)
        {
            userService = new UserService(id);
            chatService = new ChatService(c.Id);
            chat = c;
        }
        
        public List<Message> GetAllMessages()
        {
            if (chat == null) return null;
            if (chat.Messages == null) return null;
            return chat.Messages.Messages;
        }

        public int Count()
        {
            return GetAllMessages().Count;
        }

        public string lastId()
        {
            List<Message> messages = GetAllMessages();
            int len = messages.Count;
            string lastId = messages[len - 1].Id;
            return lastId + 1;
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
            Message message = Get(id);
            if (message != null)
            {
                chat.Messages.Edit(message, sent, content, created);

                // Checking is the message was sent by the user (and not to the user).
                if (sent)
                {
                    chat.Contact.Last = message.Content;
                    chat.Contact.LastDate = message.Created;
                }
            }
        }

        public void Delete(string id)
        {
            Message message = Get(id);
            if (message != null)
            {
                chatService.DeleteMessage(chat, message);
            }
        }

        /*public void sendTo(string id, string content)
        {
            User user = userService.Get(id);
            if (user == null) return; // Checking if the user exists

            userService.updateUser(id, chat.Contact, content, DateTime.Now);

            SendMessage(content, false);
        }*/

        public void SendMessage(string content, bool sent)
        {
            if (content != null)
            {
                Message message = new Message();
                int len = GetAllMessages().Count;

                message.Id = len.ToString();
                message.Created = DateTime.Now;
                message.Content = content;
                message.Sent = sent;

                chat.Messages.Add(message);

                // Checking is the message was sent by the user (and not to the user).
                if (sent)
                {
                    chat.Contact.Last = message.Content;
                    chat.Contact.LastDate = message.Created;
                }
            }
        }
    }
}
