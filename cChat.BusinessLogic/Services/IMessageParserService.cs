using cChat.Core.DTOs;
using Microsoft.AspNetCore.Identity;

namespace cChat.BusinessLogic.Services
{
    public interface IMessageParserService
    {
        ParsedChatMessage Parse(string message, IdentityUser user);
    }
}