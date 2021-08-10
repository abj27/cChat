using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cChat.BusinessLogic.Services;
using cChat.Core.DTOs;
using cChat.Data.Entities;
using cChat.Data.Repositories;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using MessageTypes = cChat.Core.DTOs.MessageTypes;


namespace cChat.Portal.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IBus _busService;
        private readonly IMessageParserService _messageParserService;
        private UserManager<IdentityUser> _userManager;
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatHub(IBus busService, IMessageParserService messageParserService, UserManager<IdentityUser> userManager, IChatMessageRepository chatMessageRepository)
        {
            _busService = busService;
            _messageParserService = messageParserService;
            _userManager = userManager;
            _chatMessageRepository = chatMessageRepository;
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
            _chatMessageRepository.Insert(new ChatMessage{
                UserId = parsedMessage.Sender,
                ChatRoomId = 1,
                Message = parsedMessage.Text
            });;
            await Clients.All.SendAsync("ReceiveMessage", parsedMessage);
        }
        private async Task HandleBotAction(ParsedChatMessage parsedChatMessage)
        {
            var endPoint = await _busService.GetSendEndpoint(new Uri("rabbitmq://localhost/robotActionsQueue"));
            await endPoint.Send(parsedChatMessage);
        }
    }

}
