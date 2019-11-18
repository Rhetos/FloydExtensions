using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(InitializationConcept))]
    public class TypeScriptGeneratorInitialCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            codeBuilder.InsertCode(CodeSnippet);
        }
        private string CodeSnippet =
            @"
import { RhetosDataStructure } from '@omega-ng/rhetos';
";
    }
}
