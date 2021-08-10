using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using cChat.BusinessLogic.Services;
using cChat.Core.DTOs;
using cChat.Data.Entities;
using cChat.Data.Repositories;
using cChat.Data.Services;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly ITransactionService _transactionService;

        public ChatHub(IBus busService, IMessageParserService messageParserService, UserManager<IdentityUser> userManager, IChatMessageRepository chatMessageRepository, IChatRoomRepository chatRoomRepository, ITransactionService transactionService)
        {
            _busService = busService;
            _messageParserService = messageParserService;
            _userManager = userManager;
            _chatMessageRepository = chatMessageRepository;
            _chatRoomRepository = chatRoomRepository;
            _transactionService = transactionService;
        }

        public async Task SendMessage(string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var parsedMessage = _messageParserService.Parse(message, user);
            await SendSystemMessage(parsedMessage);
        }

        public async Task SendSystemMessage(ParsedChatMessage parsedMessage)
        {
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
                case MessageTypes.ErrorMessage:
                {
                    await HandleErrorMessage(parsedMessage);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task HandleErrorMessage(ParsedChatMessage parsedMessage)
        {
            await Clients.All.SendAsync("ReceiveMessage", parsedMessage);
        }

        private async Task HandleChatMessage(ParsedChatMessage parsedMessage)
        {
            await _transactionService.InTransaction(async () =>
            {
                var entity =_chatMessageRepository.Insert(new ChatMessage
                {
                    UserId = parsedMessage.Sender,
                    //We can only have a single chat room in the system at the moment 
                    ChatRoomId = _chatRoomRepository.GetAll().Select(x =>x.Id).First(),
                    Message = parsedMessage.Text
                });
                await Clients.All.SendAsync("ReceiveMessage", parsedMessage);
                return entity;
            },
            onError:async     () =>{
                await Clients.Caller.SendAsync("ReceiveMessage", new ParsedChatMessage{
                   Type = MessageTypes.ErrorMessage,
                   SenderName = "System",
                   Text = $"Sorry, We couldn't send the message {parsedMessage.Text}"
                });
            });
        }
        private async Task HandleBotAction(ParsedChatMessage parsedChatMessage)
        {
            var endPoint = await _busService.GetSendEndpoint(new Uri("rabbitmq://localhost/bot-actions-queue"));
            // await endPoint.Send(new BotAction(parsedChatMessage.Text));
            await endPoint.Send(parsedChatMessage);
        }

    }

}
