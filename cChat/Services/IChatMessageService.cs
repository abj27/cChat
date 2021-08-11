using System.Threading.Tasks;
using cChat.Core.DTOs;
using cChat.Portal.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace cChat.Portal.Services
{
    public interface IChatMessageService
    {
        Task SendParsedMessage(ParsedChatMessage parsedMessage,  IHubClients<IClientProxy> clients);
        Task SendLatestMessages(string userId, IHubClients<IClientProxy> clients);
    }
}