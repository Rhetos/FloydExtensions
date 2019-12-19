using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;
using Rhetos.TypeScriptModelGenerator.DataStructure;
using Rhetos.TypeScriptModelGenerator.DataStructure.Properties;

namespace Rhetos.TypeScriptModelGenerator.Validations
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(RequiredPropertyInfo))]
    public class RequiredPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (RequiredPropertyInfo)conceptInfo;
            codeBuilder.InsertCode(@"@Required
        ", ShortStringPropertyCodeGenerator.AttributesTag, info.Property);
        }
    }
}