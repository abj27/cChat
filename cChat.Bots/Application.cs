using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace cChat.Bots
{
    public class Application
    {
        public async Task Run()
        {
            // var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            // {
            //     cfg.ReceiveEndpoint("BotActionsQueue", e =>
            //     {
            //         e.Consumer<BotActionConsumer>(typeof(BotActionConsumer));
            //     });
            // });
            //
            // var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            //
            // await busControl.StartAsync(source.Token);
            // try
            // {
            //     Console.WriteLine("Press enter to exit");
            //
            //     await Task.Run(() => Console.ReadLine());
            // }
            // finally
            // {
            //     await busControl.StopAsync();
            // }
            //  try
            // {
            //     while (true)
            //     {
            //         string value = await Task.Run(() =>
            //         {
            //             Console.WriteLine("Enter message (or quit to exit)");
            //             Console.Write("> ");
            //             return Console.ReadLine();
            //         });
            //
            //         if("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
            //             break;
            //
            //         await busControl.Publish<ValueEntered>(new
            //         {
            //             Value = value
            //         });
            //     }
            // }
            // finally
            // {
            //     await busControl.StopAsync();
            // }
        }
    }
}