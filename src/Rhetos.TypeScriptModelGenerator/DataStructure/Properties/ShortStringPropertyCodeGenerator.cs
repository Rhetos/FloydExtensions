using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure.Properties
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ShortStringPropertyInfo))]
    public class ShortStringPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<PropertyInfo> PropertyMetaDataTag = new CsTag<PropertyInfo>("TsPropertyMetaData", TagType.Appendable, "{0}", @", 
            {0}");

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            codeBuilder.InsertPropertyCode((ShortStringPropertyInfo)conceptInfo, "string");
        }
    }
}