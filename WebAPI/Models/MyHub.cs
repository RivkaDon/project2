using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Models
{
    public class MyHub : Hub
    {
        public async Task Send(string contactId, string stringMessage, string time, string sent)
        {
            Message message = 
            MessageWithContact mc = new MessageWithContact();
            mc.Contact = contactId;
            mc.Message = message;
            mc.UserId = Global.Id;
            await Clients.All.SendAsync("Receive", mc);
        }

    }
}
