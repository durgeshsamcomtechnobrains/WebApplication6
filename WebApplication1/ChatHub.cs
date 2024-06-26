﻿using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace WebApplication1
{
    public class ChatHub : Hub
    {        
        public static readonly ConcurrentDictionary<string, string> UserConnections = new ConcurrentDictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                UserConnections[userId] = Context.ConnectionId;
                await Clients.All.SendAsync("GetOnlineUsers", UserConnections.Keys);
            }
            await base.OnConnectedAsync();
        }


        public static string GetUserConnectionId(string userId)
        {
            if (UserConnections.TryGetValue(userId, out var connectionId))
            {
                return connectionId;
            }
            return null;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                UserConnections.TryRemove(userId, out _);
                await Clients.All.SendAsync("GetOnlineUsers", UserConnections.Keys);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public Task SendNotification(string receiverId, string notification)
        {
            if (UserConnections.TryGetValue(receiverId, out var connectionId))
            {
                return Clients.Client(connectionId).SendAsync("Notification", notification);
            }
            return Task.CompletedTask;
        }

        public Task ClearNotifications(string senderId)
        {
            // Implementation for clearing notifications
            return Task.CompletedTask;
        }
    }
}
