using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ModuleInfo))]
    public class ModuleCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<ModuleInfo> MembersTag = "TsModuleMembers";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ModuleInfo) conceptInfo;
            codeBuilder.InsertCode(Code(info), TypeScriptGeneratorInitialCodeGenerator.TsMembersTag, new TsBodyInfo());
        }

        private static string Code(ModuleInfo info)
        {
            return $@"
export namespace {info.Name} {{
{MembersTag.Evaluate(info)}
}}
";
        }
    }
}