using Microsoft.AspNetCore.SignalR;

namespace Y.Chat.EntityCore.Hubs
{
    public class ChatHub:Hub
    {
        public ChatHub() { }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public Task SendMessage(SendMessageModel message)
        {
            return Task.CompletedTask;
        }
    }
}
