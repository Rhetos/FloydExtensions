using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;
using Rhetos.FloydExtensions.DataStructure.Properties;

namespace Rhetos.FloydExtensions.Validations
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(MaxLengthInfo))]
    public class MaxLengthTagCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (MaxLengthInfo)conceptInfo;
            codeBuilder.InsertCode($@"\""maxLength\"": {info.Length}", PropertyCodeGenerator.PropertyMetaDataTag, info.Property);
        }
    }
}