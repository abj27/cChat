using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using cChat.Bots.Publishers;
using cChat.Bots.RobotActionHandlers;
using cChat.Bots.Services;
using MassTransit;

namespace cChat.Bots
{
    static class Program
    {
        private static IContainer GetDependencyInjectionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SendMessageService>().As<ISendMessageService>();
            builder.RegisterType<ActionHandlerService>().As<IActionHandlerService>();
            builder.RegisterType<Application>();
            builder.RegisterAssemblyTypes(typeof(DefaultActionHandler).Assembly).Where(x => x.Name.EndsWith("ActionHandler")).AsImplementedInterfaces(); 
            builder.AddMassTransit(x =>{
                x.AddConsumer<BotActionConsumer>();
                x.UsingRabbitMq((context, config) =>{
                    config.Host("localhost");
                    config.ReceiveEndpoint("bot-actions-queue", e =>
                    {
                        e.ConfigureConsumer<BotActionConsumer>(context);
                    });
                });
            });
            var container = builder.Build();
            return container;
        }

        public static async Task Main()
        {
            var busControl = await SetupMessageBroker(GetDependencyInjectionRoot());
            try
            {
                await GetDependencyInjectionRoot().Resolve<Application>().Run();
                Console.WriteLine("Press enter to exit");
                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }

        private static async Task<IBusControl> SetupMessageBroker(IContainer container)
        {
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var busControl = container.Resolve<IBusControl>();
            await busControl.StartAsync(source.Token);
            busControl.Start();
            return busControl;
        }
    }

    internal class ValueEntered
    {
        public string Value { get; set; }
    }
}
