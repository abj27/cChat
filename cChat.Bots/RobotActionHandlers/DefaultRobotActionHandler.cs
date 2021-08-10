using System.Threading.Tasks;
using cChat.Bots.Services;
using cChat.Core.DTOs;

namespace cChat.Bots.RobotActionHandlers
{
    public class DefaultActionHandler: RobotActionHandler, IRobotIActionHandler
    {
        public DefaultActionHandler(ISendMessageService sendMessageService) : base(sendMessageService)
        { }
        public override string Key => null; 
        public override  async Task HandleMessage(string message)
        {
            await SendMessageService.Send(new ParsedChatMessage
            {
                Type = MessageTypes.ErrorMessage,
                Text = "Bot not found",
                SenderName = "System",
                Sender = null
            });
        }
    }
}