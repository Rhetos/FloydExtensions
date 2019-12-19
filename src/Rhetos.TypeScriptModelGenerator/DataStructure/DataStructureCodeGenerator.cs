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
        public static readonly CsTag<DataStructureInfo> ImplementsTag = new CsTag<DataStructureInfo>("TsImplements", TagType.Appendable, "implements {0}", ", {0}");
        public static readonly CsTag<DataStructureInfo> StructureMetaDataTag = new CsTag<DataStructureInfo>("TsStructureMetaData", TagType.Appendable, "{0}", @", 
        {0}");

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            DataStructureInfo info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertCode(Code(info), ModuleCodeGenerator.Members, info.Module);
            codeBuilder.InsertCode($"id: '{info.Module.Name}/{info.Name}'", StructureMetaDataTag, info);
        }

        private static string Code(DataStructureInfo info)
        {
            return $@"
    @Structure({{
        {StructureMetaDataTag.Evaluate(info)}
    }})
    export class {info.Name} extends RhetosStructureBase {ImplementsTag.Evaluate(info)} {{
        {MembersTag.Evaluate(info)}
    }}
";
        }
    }
}