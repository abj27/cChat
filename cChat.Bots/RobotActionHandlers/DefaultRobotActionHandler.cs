using cChat.Bots.Services;
using cChat.Core.DTOs;

namespace cChat.Bots.RobotActionHandlers
{
    public class DefaultActionHandler:IRobotIActionHandler
    {
        private readonly ISendMessageService _sendMessageService;

        public DefaultActionHandler(ISendMessageService sendMessageService)
        {
            _sendMessageService = sendMessageService;
            Key = null;
        }
        public void Process(string message)
        {
           _sendMessageService.Send(new ParsedChatMessage{
               Type = MessageTypes.ErrorMessage,
               Text = "Bot not found",
               SenderName = "System",
               Sender = null 
           });;
        }
        public string Key { get;}
    }
}