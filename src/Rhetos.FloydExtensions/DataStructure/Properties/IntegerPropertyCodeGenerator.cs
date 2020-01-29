using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(IntegerPropertyInfo))]
    public class IntegerPropertyCodeGenerator : PropertyCodeGenerator
    {
        protected override string JavaScriptType => "number";

        public IntegerPropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}