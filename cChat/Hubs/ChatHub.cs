using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cChat.BusinessLogic.Services;
using cChat.Core.DTOs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;


namespace cChat.Portal.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IBus _busService;
        private readonly IMessageParserService _messageParserService;
        private UserManager<IdentityUser> _userManager;

        public ChatHub(IBus busService, IMessageParserService messageParserService, UserManager<IdentityUser> userManager)
        {
            _busService = busService;
            _messageParserService = messageParserService;
             _userManager = userManager;
        }

        public async Task SendMessage(string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var parsedMessage = _messageParserService.Parse(message, user);
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

}
