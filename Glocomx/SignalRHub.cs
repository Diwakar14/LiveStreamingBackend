﻿using Glocomx.ChatServices;
using Glocomx.ChatServices.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Glocomx
{
    public class SignalRHub:Hub
    {

        private static int Count = 0;
        private readonly InMemoryChatRoomService _chatRoomService;
        public SignalRHub()
        {
            _chatRoomService = new InMemoryChatRoomService();
        }

        public async override Task OnConnectedAsync()
        {
            var httpContext = this.Context.GetHttpContext();
            var streamId = httpContext.Request.Query["streamId"];

            var roomId = await _chatRoomService.CreateRoom(Context.ConnectionId, streamId);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
            
            Count++;
            await base.OnConnectedAsync();
            await Clients.Group(roomId.ToString()).SendAsync("onConnectionChange", Count, roomId.ToString());
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = this.Context.GetHttpContext();
            var streamId = httpContext.Request.Query["streamId"];

            var roomId = await _chatRoomService.CreateRoom(Context.ConnectionId, streamId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());

            Count--;
            await base.OnDisconnectedAsync(exception);
            await Clients.Group(roomId.ToString()).SendAsync("onConnectionChange", Count, roomId.ToString());
        }


        public async Task SendMessage(string streamId,string userId, string name, string text)
        {


            var message = new ChatMessage
            {
                SenderName = name,
                Text = text,
                UserId = userId,
                SentAt = DateTimeOffset.UtcNow
            };


            var newMessage = JsonSerializer.Serialize(message);

            // Broadcast to all clients
            await Clients.Group(streamId).SendAsync("onReceiveMessage", newMessage.ToString());

        }

        public async Task SendRtcMessage(string roomId, object message)
        {
            await Clients.Group(roomId).SendAsync("onRtcMessage", message);
        }
        
    }
}