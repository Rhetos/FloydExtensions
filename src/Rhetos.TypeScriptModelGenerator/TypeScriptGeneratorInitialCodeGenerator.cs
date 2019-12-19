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
        public static readonly CsTag<TsBodyInfo> Members = "TsMembers";
        public static readonly CsTag<TsBodyInfo> TypeMapping = new CsTag<TsBodyInfo>("TsTypeMapping", TagType.Appendable, "{0}", @", 
    {0}");

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            codeBuilder.InsertCode(_codeSnippet);
        }
        private readonly string _codeSnippet =
            $@"
import {{ RhetosDataStructure }} from '@omega-ng/rhetos';
{Members.Evaluate(new TsBodyInfo())}

export const typeMapping: {{ [key: string]: typeof RhetosStructureBase }} = {{
    {TypeMapping.Evaluate(new TsBodyInfo())}
}};
";
    }

    public class TsBodyInfo : IConceptInfo
    {
        [ConceptKey]
        public string Name { get; set; } = "TypeScriptBody";
    }
}
