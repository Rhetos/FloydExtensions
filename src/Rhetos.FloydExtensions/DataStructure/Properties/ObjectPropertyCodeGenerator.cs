using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.ComplexEntity.ComplexParameter;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ObjectPropertyDslModelInfo))]
    public class ObjectDslPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ObjectPropertyDslModelInfo) conceptInfo;
            codeBuilder.InsertCode($@"
        {info.Name}?: {info.PropertyStructure.Module.Name}.{info.PropertyStructure.Name};", DataStructureCodeGenerator.MembersTag, info.DataStructure);

            codeBuilder.InsertCode($@"\""{info.Name}\"": {{ \""type\"": \""Object\"",  \""keyOfComplexMember\"": \""{info.PropertyStructure.Module.Name}/{info.PropertyStructure.Name}\""}}", DataStructureCodeGenerator.NavigationalPropertiesMetaDataTag, info.DataStructure);
        }
    }
    
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ObjectPropertyInfo))]
    public class ObjectPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ObjectPropertyInfo) conceptInfo;
            codeBuilder.InsertCode($@"
        {info.Name}?: {info.DataStructure.Module.Name}.{info.DataStructure.Name};", DataStructureCodeGenerator.MembersTag, info.DataStructure);

            codeBuilder.InsertCode($@"\""{info.Name}\"": {{ \""type\"": \""Object\"",  \""keyOfComplexMember\"": \""{info.DataStructure.Module.Name}/{info.DataStructure.Name}\""}}", DataStructureCodeGenerator.NavigationalPropertiesMetaDataTag, info.DataStructure);
        }
    }
}