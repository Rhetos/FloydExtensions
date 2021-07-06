using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptSupportedType))]
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ShortStringPropertyInfo))]
    public class ShortStringPropertyCodeGenerator : PropertyCodeGenerator, ITypeScriptSupportedType
    {
	    public override string TypeScriptType => "string";
	    public string PropertyType => "ShortString";

	    public ShortStringPropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}