using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions.DataStructure.Properties
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(LongStringPropertyInfo))]
    public class LongStringPropertyCodeGenerator : PropertyCodeGenerator
    {
        protected override string JavaScriptType => "string";

        public LongStringPropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }
    }
}