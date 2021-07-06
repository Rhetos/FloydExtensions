using System.Linq;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace Rhetos.FloydExtensions
{
    public abstract class PropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<PropertyInfo> PropertyMetaDataTag = new CsTag<PropertyInfo>("TsPropertyMetaData", TagType.Appendable, "{0}", @", {0}");

        protected readonly IDslModel DslModel;

        public abstract string TypeScriptType { get; }

        protected virtual string NameSufix => string.Empty;

        protected PropertyCodeGenerator(IDslModel dslModel)
        {
            DslModel = dslModel;
        }

        public virtual void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var isRequired = DslModel.FindByType<RequiredPropertyInfo>().Any(x => x.Property == conceptInfo);
            codeBuilder.InsertPropertyCode((PropertyInfo)conceptInfo, TypeScriptType, NameSufix, isRequired);
        }
    }
}