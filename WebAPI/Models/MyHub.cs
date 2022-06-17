using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Models
{
    public class MyHub : Hub
    {
        public async Task Send(string message, string contactId)
        {
            // string id = User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value; ?
            await Clients.All.SendAsync("Receive", message, contactId, Global.Id);
        }
    }
}
