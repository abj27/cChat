using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cChat.Core.DTOs;
using MassTransit;

namespace cChat.Bots.Services
{
    public interface ISendMessageService
    {
        Task Send(ParsedChatMessage chatMessage);
    }

    public class SendMessageService : ISendMessageService
    {
        private readonly IBus _busService;

        public SendMessageService(IBus busService)
        {
            _busService = busService;
        }
        public async Task Send(ParsedChatMessage chatMessage)
        {
            var endPoint = await _busService.GetSendEndpoint(new Uri("rabbitmq://localhost/bot-chat-message-queue"));
            await endPoint.Send(chatMessage);
        }
    }
}
