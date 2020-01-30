using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(InitializationConcept))]
    public class TypeScriptGeneratorInitialCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<TsBodyInfo> TsMembersTag = "TsMembers";
        public static readonly CsTag<TsBodyInfo> StructureMetaDataTag = new CsTag<TsBodyInfo>("StructureMetaDataTag", TagType.Appendable, "{0}", @", 
    {0}");

        public const string FileSplitTag = "--- File Split ---";
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            codeBuilder.InsertCode(_codeSnippet);
        }
        private readonly string _codeSnippet =
$@"import {{ createStructureInfo, createFunctionInfo, createComplexInfo, createComplexGetInfo }} from '@floyd-ng/rhetos';
{TsMembersTag.Evaluate(new TsBodyInfo())}
{FileSplitTag}

{{{StructureMetaDataTag.Evaluate(new TsBodyInfo())}
}}
";
    }

    public class TsBodyInfo : IConceptInfo
    {
        [ConceptKey]
        public string Name { get; set; } = "TypeScriptBody";
    }
}
