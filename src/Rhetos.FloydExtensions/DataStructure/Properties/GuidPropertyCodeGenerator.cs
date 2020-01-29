using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(GuidPropertyInfo))]
    public class GuidPropertyCodeGenerator : PropertyCodeGenerator
    {
        protected override string JavaScriptType => "string";

        public GuidPropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}