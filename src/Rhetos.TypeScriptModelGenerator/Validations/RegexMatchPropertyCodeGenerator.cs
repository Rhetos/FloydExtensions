using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;
using Rhetos.TypeScriptModelGenerator.DataStructure.Properties;

namespace Rhetos.TypeScriptModelGenerator.Validations
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(RegExMatchInfo))]
    public class RegexMatchPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (RegExMatchInfo)conceptInfo;
            codeBuilder.InsertCode($"regex: {{expression: '{info.RegularExpression}', message: '{info.ErrorMessage}'}}", PropertyCodeGenerator.PropertyMetaDataTag, info.Property);
        }
    }
}