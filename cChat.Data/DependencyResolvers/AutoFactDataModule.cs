using Autofac;
using cChat.Data.Repositories;
using cChat.Data.Services;

namespace cChat.Data.DependencyResolvers
{
    public class AutoFacDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChatRoomRepository>().As<IChatRoomRepository>();
            builder.RegisterType<ChatMessageRepository>().As<IChatMessageRepository>();
            builder.RegisterType<TransactionService>().As<ITransactionService>();
            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>();
        }
    }
}
