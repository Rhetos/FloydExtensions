using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(TypeScriptFilterInfo))]
    public class FilterCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (TypeScriptFilterInfo)conceptInfo;
            codeBuilder.InsertCode("IRhetosFilterParameter", DataStructureCodeGenerator.ImplementsTag, info.DataStructure);
        }
    }
}