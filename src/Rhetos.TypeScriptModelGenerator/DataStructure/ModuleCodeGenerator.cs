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
        public static readonly CsTag<ModuleInfo> Members = "Members";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ModuleInfo) conceptInfo;
            codeBuilder.InsertCode(Code(info));
        }

        private static string Code(ModuleInfo info)
        {
            return $@"
namespace {info.Name} {{
{Members.Evaluate(info)}
}}
";
        }
    }
}