using System.Collections.Generic;
using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.ComplexEntity.ComplexParameter;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ListOfDslModelInfo))]
    public class ListOfDslPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ListOfDslModelInfo)conceptInfo;
            codeBuilder.InsertCode($@"
        {info.Name}?: {info.PropertyStructure.Module.Name}.{info.PropertyStructure.Name}[];", DataStructureCodeGenerator.MembersTag, info.DataStructure);

            codeBuilder.InsertCode($@"\""{info.Name}\"": {{ \""type\"": \""ListOf\"",  \""keyOfComplexMember\"": \""{info.PropertyStructure.Module.Name}/{info.PropertyStructure.Name}\""}}", DataStructureCodeGenerator.NavigationalPropertiesMetaDataTag, info.DataStructure);
        }
    }

    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ListOfInfo))]
    public class ListOfPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
	    private readonly IEnumerable<ITypeScriptSupportedType> _supportedPropertyTypes;

	    public ListOfPropertyCodeGenerator(IEnumerable<ITypeScriptSupportedType> supportedPropertyTypes)
	    {
		    _supportedPropertyTypes = supportedPropertyTypes;
	    }

	    public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ListOfInfo)conceptInfo;
            codeBuilder.InsertCode($@"
        {info.Name}?: {_supportedPropertyTypes.GetTypeScriptType(info.PropertyType)}[];", DataStructureCodeGenerator.MembersTag, info.DataStructure);

            codeBuilder.InsertCode($@"\""{info.Name}\"": {{ \""type\"": \""ListOf\"",  \""keyOfComplexMember\"": \""{info.DataStructure.Module.Name}/{info.DataStructure.Name}\""}}", DataStructureCodeGenerator.NavigationalPropertiesMetaDataTag, info.DataStructure);
        }
    }
}