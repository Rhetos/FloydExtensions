using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(DataStructureInfo))]
    public class DataStructureCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<DataStructureInfo> MembersTag = "TsProperties";
        public static readonly CsTag<DataStructureInfo> ImplementsTag = new CsTag<DataStructureInfo>("TsImplements", TagType.Appendable, "extends {0}", ", {0}");
        public static readonly CsTag<DataStructureInfo> StructureMetaDataTag = new CsTag<DataStructureInfo>("TsStructureMetaDataTag", TagType.Appendable, "{0}", @", 
        {0}");
        public static readonly CsTag<DataStructureInfo> PropertiesMetaDataTag = new CsTag<DataStructureInfo>("TsPropertiesMetaDataTag", TagType.Appendable, "{0}", @", {0}");

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            DataStructureInfo info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertCode(Code(info), ModuleCodeGenerator.MembersTag, info.Module);
            codeBuilder.InsertCode(MetaData(info), TypeScriptGeneratorInitialCodeGenerator.StructureMetaDataTag, new TsBodyInfo());
            codeBuilder.InsertCode($"key: '{info.Module.Name}/{info.Name}'", StructureMetaDataTag, info);
        }

        private static string Code(DataStructureInfo info)
        {
            return $@"
    export const {info.Name}Key = '{info.Module.Name}/{info.Name}';
    export interface {info.Name} {ImplementsTag.Evaluate(info)}{{{MembersTag.Evaluate(info)}
    }}
";
        }

        private static string MetaData(DataStructureInfo info)
        {
            return $@"
    ""{info.Module.Name}/{info.Name}"": ""{{
        {StructureMetaDataTag.Evaluate(info)},
        properties: {{{PropertiesMetaDataTag.Evaluate(info)}
        }}
    }}""";
        }
    }
}