using Microsoft.AspNetCore.SignalR;

namespace Chatroom_Example.Core
{
    public class ChatHub : Hub
    {
        public const string HubUrl = "/chat";

        public async Task Broadcast()
        {
            await Clients.All.SendAsync("Broadcast");
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.UserIdentifier} connected");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception != null)
            {
                Console.WriteLine($"Disconnected {exception.Message} {Context.UserIdentifier}");
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
