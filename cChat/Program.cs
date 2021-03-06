using Autofac;
using Autofac.Extensions.DependencyInjection;
using cChat.BusinessLogic.DependencyResolvers;
using cChat.Core.DependencyResolvers;
using cChat.Data.DependencyResolvers;
using cChat.Portal.Hubs;
using cChat.Portal.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace cChat.Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

       public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutoFacCoreModule());
                    builder.RegisterModule(new AutoFacDataModule());
                    builder.RegisterModule(new AutoFactBusinessLogicModule());
                    builder.RegisterType<ChatMessageService>().As<IChatMessageService>();
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
