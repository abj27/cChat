using Autofac;
using cChat.Data.Repositories;

namespace cChat.Data.DependencyResolvers
{
    public class AutoFacDataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChatRoomRepository>().As<IChatRoomRepository>();
        }
    }
}
