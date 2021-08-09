using cChat.Core.DTOs;
using Microsoft.AspNetCore.Identity;

namespace cChat.BusinessLogic.Services
{
    public class MessageParserService : IMessageParserService
    {
        public ParsedChatMessage Parse(string message, IdentityUser user)
        {
            return  new ParsedChatMessage{ 
                Text = message,
                Type = MessageTypes.ChatMessage, 
                SenderName = user.NormalizedUserName,
                Sender = user.Id
            };
        }
    }
}