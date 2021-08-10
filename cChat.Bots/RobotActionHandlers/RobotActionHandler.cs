using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cChat.Bots.Services;
using cChat.Core.DTOs;

namespace cChat.Bots.RobotActionHandlers
{
    public abstract class RobotActionHandler
    {
        public abstract string Key {get;}
        public async Task Process(string message){
            try
            {
                await HandleMessage(message);
            }
            catch(Exception e)
            {
                await OnError();
            }
        }

        public virtual async Task OnError()
        {
            await SendMessageService.Send(new ParsedChatMessage
            {
                Type = MessageTypes.ErrorMessage,
                SenderName = "System",
                Text = $"We couldn't process your {Key} action",
                Sender= null
            });
        }

        public abstract Task HandleMessage(string message);


        protected readonly ISendMessageService SendMessageService;

        protected RobotActionHandler(ISendMessageService sendMessageService)
        {
            SendMessageService = sendMessageService;
        }

    }
}
