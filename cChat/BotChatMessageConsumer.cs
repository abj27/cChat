using System.Threading.Tasks;
using cChat.Core.DTOs;
using cChat.Portal.Hubs;
using cChat.Portal.Services;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace cChat.Portal
{
    public class BotChatMessageConsumer :  IConsumer<ParsedChatMessage>
    {
        private readonly IChatMessageService _chatMessageService;
        private IHubContext<ChatHub> _chatHub;

        public BotChatMessageConsumer(IChatMessageService chatMessageService, IHubContext<ChatHub> chatHub)
        {
            _chatMessageService = chatMessageService;
            _chatHub = chatHub;
        }

        public async Task Consume(ConsumeContext<ParsedChatMessage> context)
        {
             await _chatMessageService.SendParsedMessage(context.Message, _chatHub.Clients);
        }
    }

}