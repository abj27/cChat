using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace cChat.Bots
{
    class Program
    {
        public static async Task Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ReceiveEndpoint("BotActionsQueue", e =>
                {
                    e.Consumer<BotActionConsumer>();
                });
            });

            // var busControl = Bus.Factory.CreateUsingInMemory(cfg =>
            // {
            //     cfg.ReceiveEndpoint("event-listener", e =>
            //     {
            //         e.Consumer<EventConsumer>();
            //     });
            // });
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
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

        class EventConsumer :
            IConsumer<ValueEntered>
        {
            public async Task Consume(ConsumeContext<ValueEntered> context)
            {
                Console.WriteLine("Value: {0}", context.Message.Value);
            }
        }
    }

    internal class ValueEntered
    {
        public string Value { get; set; }
    }
}
