using System.ComponentModel.Composition;
using Autofac;

namespace Rhetos.FloydExtensions
{
    public class TypeScriptGeneratorModuleConfiguration : Module
    {
        [Export(typeof(Module))]
        protected override void Load(ContainerBuilder builder)
        {
            Extensibility.Plugins.FindAndRegisterPlugins<ITypeScriptGeneratorPlugin>(builder);
            builder.RegisterType<MetadataProvider>().SingleInstance();
            base.Load(builder);
        }
    }
}
