using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure.Properties
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(MoneyPropertyInfo))]
    public class MoneyPropertyCodeGenerator : PropertyCodeGenerator
    {
        protected override string JavaScriptType => "number";

        public MoneyPropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}