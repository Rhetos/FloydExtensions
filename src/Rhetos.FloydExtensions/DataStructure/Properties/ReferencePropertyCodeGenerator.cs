using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions.DataStructure.Properties
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ReferencePropertyInfo))]
    public class ReferencePropertyCodeGenerator : PropertyCodeGenerator
    {
        protected override string JavaScriptType => "string";

        protected override string NameSufix => "ID";

        public override void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            base.GenerateCode(conceptInfo, codeBuilder);
            var info = (ReferencePropertyInfo) conceptInfo;
            codeBuilder.InsertCode($@"\""referenceKey\"": \""{info.Referenced.Module.Name}/{info.Referenced.Name}\""", PropertyMetaDataTag, info);
        }

        public ReferencePropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}