using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cChat.BusinessLogic.Services;
using cChat.Portal.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;


namespace cChat.Portal.Hubs
{
    public class ChatHub: Hub
    {
        private readonly IMessageParserService _messageParserService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IChatMessageService _chatMessageService;

        public ChatHub(IMessageParserService messageParserService, UserManager<IdentityUser> userManager, IChatMessageService chatMessageService)
        {
            _messageParserService = messageParserService;
            _userManager = userManager;
            _chatMessageService = chatMessageService;
        }

        public async Task SendMessage(string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var parsedMessage = _messageParserService.Parse(message, user);
            await _chatMessageService.SendParsedMessage(parsedMessage, this.Clients);
        }
    }

}
