using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptSupportedType))]
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(DatePropertyInfo))]
    public class DatePropertyCodeGenerator : PropertyCodeGenerator, ITypeScriptSupportedType
    {
        public override string TypeScriptType => "Date";
        public string PropertyType => "Date";


        public DatePropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}