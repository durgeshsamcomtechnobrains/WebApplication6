using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using WebApplication1.Model;

namespace WebApplication1
{
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> UserSocketMap = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentBag<Notification> Notifications = new ConcurrentBag<Notification>();

        public override Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId) && userId != "undefined")
            {
                UserSocketMap[userId] = Context.ConnectionId;
                Clients.All.SendAsync("getOnlineUsers", UserSocketMap.Keys);
            }
            return base.OnConnectedAsync();
        }

        public async Task SendNotification(Notification notification)
        {
            if (UserSocketMap.TryGetValue(notification.ReceiverId, out var receiverSocketId))
            {
                await Clients.Client(receiverSocketId).SendAsync("notification", notification);
            }
        }

        public Task ClearNotifications(string senderId)
        {
            var updatedNotifications = Notifications.Where(notification => notification.SenderId != senderId).ToList();
            Notifications.Clear();
            foreach (var notification in updatedNotifications)
            {
                Notifications.Add(notification);
            }
            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId) && UserSocketMap.ContainsKey(userId))
            {
                UserSocketMap.TryRemove(userId, out _);
                Clients.All.SendAsync("getOnlineUsers", UserSocketMap.Keys);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
