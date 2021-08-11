using System;
using System.Linq;
using System.Threading.Tasks;
using cChat.Core.DTOs;
using cChat.Data.Entities;
using cChat.Data.Repositories;
using cChat.Data.Services;
using cChat.Portal.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace cChat.Portal.Services
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly ITransactionService _transactionService;
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IIdentityUserRepository _userRepository;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IBus _busService;

        public ChatMessageService( ITransactionService transactionService, IChatMessageRepository chatMessageRepository, IIdentityUserRepository userRepository, IChatRoomRepository chatRoomRepository, IBus busService)
        {
            _transactionService = transactionService;
            _chatMessageRepository = chatMessageRepository;
            _userRepository = userRepository;
            _chatRoomRepository = chatRoomRepository;
            _busService = busService;
        }

        public async Task SendParsedMessage(ParsedChatMessage parsedMessage, IHubClients<IClientProxy> clients)
        {
            switch (parsedMessage.Type)
            {
                case MessageTypes.BotAction:
                    await HandleBotAction(parsedMessage);
                    break;
                case MessageTypes.ChatMessage:
                {
                    await HandleChatMessage(parsedMessage, clients);
                    break;
                }
                case MessageTypes.ErrorMessage:
                {
                    await HandleErrorMessage(parsedMessage, clients);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private async Task HandleErrorMessage(ParsedChatMessage parsedMessage, IHubClients<IClientProxy> clients)
        {
            await clients.All.SendAsync("ReceiveMessage", parsedMessage);
        }

        private async Task HandleChatMessage(ParsedChatMessage parsedMessage, IHubClients<IClientProxy> clients)
        {
            await _transactionService.InTransaction(async () =>
                {
                    var entity = _chatMessageRepository.Insert(new ChatMessage
                    {
                        UserId = parsedMessage.Sender?? _userRepository.GetSystemUser().Id,
                        //We can only have a single chat room in the system at the moment 
                        ChatRoomId = _chatRoomRepository.GetAll().Select(x =>x.Id).First(),
                        Message = parsedMessage.Text
                    });
                    await clients.All.SendAsync("ReceiveMessage", parsedMessage);
                    return entity;
                },
                onError:async     () =>{
                    await clients.User(parsedMessage.Sender).SendAsync("ReceiveMessage", new ParsedChatMessage{
                        Type = MessageTypes.ErrorMessage,
                        SenderName = "System",
                        Text = $"Sorry, We couldn't send the message {parsedMessage.Text}"
                    });
                });
        }

        private async Task HandleBotAction(ParsedChatMessage parsedChatMessage)
        {
            var endPoint = await _busService.GetSendEndpoint(new Uri("rabbitmq://localhost/bot-actions-queue"));
            await endPoint.Send(parsedChatMessage);
        }

    }
}