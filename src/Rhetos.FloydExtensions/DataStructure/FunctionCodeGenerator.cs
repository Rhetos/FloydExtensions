using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.ComplexEntity.Function;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions.DataStructure
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(FunctionDataStructureInfo))]
    public class FunctionCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (FunctionDataStructureInfo)conceptInfo;
            codeBuilder.InsertServiceType(info, "Action");
            codeBuilder.InsertCode($@"
    export const {info.Name}FunctionInfo = createFunctionInfo({info.Name}Info, {info.ReturnType.Name}Info);
", ModuleCodeGenerator.MembersTag, info.Module);
        }
    }
}