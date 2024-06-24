using Microsoft.AspNetCore.SignalR;

namespace WebApplication6
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Broadcast message to all clients
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
