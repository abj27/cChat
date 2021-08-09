using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cChat.Core.DTOs;
using MassTransit;

namespace cChat.Bots
{
    public class BotActionConsumer: IConsumer<BotAction>
    {
        public async Task Consume(ConsumeContext<BotAction> context)
        {
           await Task.Run(() => { 
               var a= context.Message;
           });
        }
    }

    
}
