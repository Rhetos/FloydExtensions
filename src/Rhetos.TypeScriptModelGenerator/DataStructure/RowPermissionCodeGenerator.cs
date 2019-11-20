using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(RowPermissionsReadInfo))]
    public class RowPermissionCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (RowPermissionsReadInfo)conceptInfo;
            codeBuilder.InsertCode(@"@HasReadRowPermissions
    ", DataStructureCodeGenerator.AttributesTag, info.Source);
        }
    }
}