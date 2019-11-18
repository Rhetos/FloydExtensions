using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure.Properties
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(BoolPropertyInfo))]
    public class BoolPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            codeBuilder.InsertPropertyCode((BoolPropertyInfo) conceptInfo, "boolean");
        }
    }
}