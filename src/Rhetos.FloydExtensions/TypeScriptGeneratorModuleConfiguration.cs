using System.ComponentModel.Composition;
using Autofac;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(Module))]
    public class TypeScriptGeneratorModuleConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Extensibility.Plugins.FindAndRegisterPlugins<ITypeScriptGeneratorPlugin>(builder);
            Extensibility.Plugins.FindAndRegisterPlugins<ITypeScriptSupportedType>(builder);
            builder.RegisterType<MetadataProvider>().SingleInstance();
            base.Load(builder);
        }
    }
}
