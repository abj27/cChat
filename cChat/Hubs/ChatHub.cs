using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cChat.Core.DTOs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace cChat.Portal.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IBus _busService;
        private readonly IMessageParserService _messageParserService;

        public ChatHub(IBus busService, IMessageParserService messageParserService)
        {
            _busService = busService;
            _messageParserService = messageParserService;
        }

        public async Task SendMessage(string user, string message)
        {
            var parsedMessage = _messageParserService.Parse(message);
            switch (parsedMessage.Type)
            {
                case MessageTypes.BotAction:
                    await HandleBotAction(parsedMessage);
                    break;
                case MessageTypes.ChatMessage:
                {
                    await HandleChatMessage(parsedMessage);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task HandleChatMessage(ParsedChatMessage parsedMessage)
        {
            await Clients.All.SendAsync("ReceiveMessage", parsedMessage);
        }
        private async Task HandleBotAction(ParsedChatMessage parsedChatMessage)
        {
            var endPoint = await _busService.GetSendEndpoint(new Uri("rabbitmq://localhost/robotActionsQueue"));
            await endPoint.Send(parsedChatMessage);
        }
    }

    public enum MessageTypes
    {
        BotAction,
        ChatMessage
    }

    public interface IMessageParserService
    {
        ParsedChatMessage Parse(string message);
    }

    public class ParsedChatMessage
    {
        public MessageTypes Type { get; set; }
        public string Sender { get; set; }
        public string Text { get; set; }
    }
}
