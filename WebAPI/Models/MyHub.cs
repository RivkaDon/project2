﻿using Microsoft.AspNetCore.SignalR;
using WebAPI.Services;

namespace WebAPI.Models
{
    public class MyHub : Hub
    {
        public async Task Send(string contactId, string stringMessage, string time, string sent)
        {
            MessageService ms = new MessageService();
            Message message = ms.Get(contactId, Global.Id);
            MessageWithContact mc = new MessageWithContact();
            mc.Contact = contactId;
            mc.Message = message;
            mc.UserId = Global.Id;
            await Clients.All.SendAsync("Receive", mc);

        }

    }
}
