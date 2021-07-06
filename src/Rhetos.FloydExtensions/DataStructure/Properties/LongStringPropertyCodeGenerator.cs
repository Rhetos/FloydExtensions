using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptSupportedType))]
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(LongStringPropertyInfo))]
    public class LongStringPropertyCodeGenerator : PropertyCodeGenerator, ITypeScriptSupportedType
    {
	    public string PropertyType => "LongString";
        public override string TypeScriptType => "string";

        public LongStringPropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}