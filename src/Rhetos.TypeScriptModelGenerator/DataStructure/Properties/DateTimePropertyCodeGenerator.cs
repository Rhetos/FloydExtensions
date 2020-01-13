using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure.Properties
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