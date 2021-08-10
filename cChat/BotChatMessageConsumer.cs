using System.Threading.Tasks;
using cChat.Core.DTOs;
using cChat.Portal.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace cChat.Portal
{
    public class BotChatMessageConsumer :  IConsumer<ParsedChatMessage>
    {
        private readonly IHubContext<ChatHub> _chatHub;

        // public BotChatMessageConsumer()
        // {

        // }
        public BotChatMessageConsumer(IHubContext<ChatHub> chatHub)
        {
            _chatHub = chatHub;
        }
        public async Task Consume(ConsumeContext<ParsedChatMessage> context)
        {
            await _chatHub.Clients.All.SendAsync("ReceiveMessage",context.Message);
            // var connection = new HubConnectionBuilder()
            //     .WithUrl("http://localhost:5001/ChatHub")
            //     .Build();
            // await connection.InvokeAsync("ReceiveMessage",context.Message );
            // await _chatHub.Clients.All.SendAsync("ReceiveMessage", context.Message);
        }
    }

}