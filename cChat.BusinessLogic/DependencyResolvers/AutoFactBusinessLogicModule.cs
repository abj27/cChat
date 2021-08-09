using Autofac;
using cChat.BusinessLogic.Services;
using cChat.Data.Repositories;

namespace cChat.BusinessLogic.DependencyResolvers
{
    public class AutoFactBusinessLogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChatRoomService>().As<IChatRoomService>();
            builder.RegisterType<MessageParserService>().As<IMessageParserService>();
        }
    }
}
