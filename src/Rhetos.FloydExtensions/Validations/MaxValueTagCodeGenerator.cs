using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;
using Rhetos.FloydExtensions.DataStructure.Properties;

namespace Rhetos.FloydExtensions.Validations
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(MaxValueInfo))]
    public class MaxValueTagCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (MaxValueInfo)conceptInfo;

            if (info.Property is IntegerPropertyInfo || info.Property is MoneyPropertyInfo || info.Property is DecimalPropertyInfo)
                codeBuilder.InsertCode($@"\""maxValue\"": {info.Value}", PropertyCodeGenerator.PropertyMetaDataTag, info.Property);
            else if (info.Property is DatePropertyInfo || info.Property is DateTimePropertyInfo)
                codeBuilder.InsertCode($@"\""maxValue\"": \""{info.Value}\""", PropertyCodeGenerator.PropertyMetaDataTag, info.Property);
            else
                throw new FrameworkException($"Unsupported property type '{info.Property.GetType().Name}'.");
        }
    }
}