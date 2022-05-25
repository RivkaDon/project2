using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Models
{
    public class MyHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.All.SendAsync("Receive", message);
        }

    }
}
