using Autofac;
using Autofac.Extensions.DependencyInjection;
using cChat.BusinessLogic.DependencyResolvers;
using cChat.Core.DependencyResolvers;
using cChat.Data.DependencyResolvers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace cChat.Hubs
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
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
