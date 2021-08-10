using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cChat.Bots.RobotActionHandlers;
using cChat.Bots.Services;
using cChat.Core.DTOs;
using MassTransit;

namespace cChat.Bots
{
    public class BotActionConsumer: IConsumer<ParsedChatMessage>
    {
        // private readonly IActionHandlerService _actionHandlerService;

        public BotActionConsumer()
        {
            // _actionHandlerService = actionHandlerService;
        }

        public async Task Consume(ConsumeContext<ParsedChatMessage> context)
        {
            // _actionHandlerService.Process(new BotAction(context.Message.Text));
            await Task.Run(() => { 
               var a= context.Message;
            });
        }

        // public async Task Consume(ConsumeContext<BotAction> context)
        // {
        //     Console.WriteLine(context.Message.Message);
        //     await Task.Run(() => { 
        //        var a= context.Message;
        //     });
        // }
    }
}
