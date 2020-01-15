using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions.DataStructure.Properties
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(DatePropertyInfo))]
    public class DatePropertyCodeGenerator : PropertyCodeGenerator
    {
        protected override string JavaScriptType => "Date";

        public DatePropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}