using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure.Properties
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ReferencePropertyInfo))]
    public class ReferencePropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ReferencePropertyInfo) conceptInfo;
            codeBuilder.InsertPropertyCode((ReferencePropertyInfo)conceptInfo, "string", "ID");
            codeBuilder.InsertCode($"referenceId: '{info.Referenced.Module.Name}/{info.Referenced.Name}'}}", ShortStringPropertyCodeGenerator.PropertyMetaDataTag, info);
        }
    }
}