using Microsoft.AspNetCore.SignalR;
using WebAPI.Services;

namespace WebAPI.Models
{
    public class MyHub : Hub
    {
        public async Task Send(string contactId)
        {
            MessageService ms = new MessageService();
            Message message = ms.Get(contactId, Global.Id);
            MessageWithContact mc = new MessageWithContact();
            mc.Contact = contactId;
            mc.Message = message;
            mc.UserId = Global.Id;
            Console.WriteLine("mc.contact inside send= " + mc.Contact);
            Console.WriteLine("mc.meesage inside send= " + mc.Message);
            Console.WriteLine(" mc.UserId inside send= " + mc.UserId);

            await Clients.All.SendAsync("Receive", mc.UserId, mc.Contact);

        }

    }
}
