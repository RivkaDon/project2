using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Models
{
    public class MyHub : Hub
    {
        public async Task Send(string message, string contactId)
        {
            await Clients.All.SendAsync("Receive", message, contactId, Global.Id);
        }

    }
}
