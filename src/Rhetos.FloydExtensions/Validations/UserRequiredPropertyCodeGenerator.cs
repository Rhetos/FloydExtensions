using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;
using Rhetos.FloydExtensions.DataStructure.Properties;

namespace Rhetos.FloydExtensions.Validations
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(UserRequiredPropertyInfo))]
    public class UserRequiredPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (UserRequiredPropertyInfo)conceptInfo;
            codeBuilder.InsertCode(@"\""required\"": true", PropertyCodeGenerator.PropertyMetaDataTag, info.Property);
        }
    }
}