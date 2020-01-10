using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ModuleInfo))]
    public class ModuleCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<ModuleInfo> MembersTag = "TsModuleMembers";
        public static readonly CsTag<ModuleInfo> MetaDataTag = new CsTag<ModuleInfo>("TsModuleMetaDataTag", TagType.Appendable, "{0}", @", {0}");

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ModuleInfo) conceptInfo;
            codeBuilder.InsertCode(Code(info), TypeScriptGeneratorInitialCodeGenerator.Members, new TsBodyInfo());
        }

        private static string Code(ModuleInfo info)
        {
            return $@"
export module {info.Name} {{
    export const _metadataMap: StructureMetadataMap = {{{MetaDataTag.Evaluate(info)}
    }}
{MembersTag.Evaluate(info)}
}}
";
        }
    }
}