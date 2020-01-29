using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(DateTimePropertyInfo))]
    public class DateTimePropertyCodeGenerator : PropertyCodeGenerator
    {
        protected override string JavaScriptType => "Date";

        public DateTimePropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}