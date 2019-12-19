using System.ComponentModel.Composition;
using Autofac;

namespace Rhetos.TypeScriptModelGenerator
{
    public class TypeScriptGeneratorModuleConfiguration : Module
    {
        [Export(typeof(Module))]
        protected override void Load(ContainerBuilder builder)
        {
            Extensibility.Plugins.FindAndRegisterPlugins<ITypeScriptGeneratorPlugin>(builder);
            base.Load(builder);
        }
    }
}
