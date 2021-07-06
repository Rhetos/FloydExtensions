using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptSupportedType))]
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(IntegerPropertyInfo))]
    public class IntegerPropertyCodeGenerator : PropertyCodeGenerator, ITypeScriptSupportedType
    {
        public override string TypeScriptType => "number";
        public string PropertyType => "Integer";


        public IntegerPropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}