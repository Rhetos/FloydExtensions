using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure.Properties
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(DateTimePropertyInfo))]
    public class DateTimePropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            codeBuilder.InsertPropertyCode((DateTimePropertyInfo)conceptInfo, "Date");
        }
    }
}