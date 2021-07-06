using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptSupportedType))]
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(BoolPropertyInfo))]
    public class BoolPropertyCodeGenerator : PropertyCodeGenerator, ITypeScriptSupportedType
    {
        public override string TypeScriptType => "boolean";
        public string PropertyType => "Bool";


        public BoolPropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}