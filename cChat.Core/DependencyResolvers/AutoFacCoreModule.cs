using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace cChat.Core.DependencyResolvers
{
    public class AutoFacCoreModule :  Module
    {
        protected override void Load(ContainerBuilder builder)
        {
        }
    }
}
