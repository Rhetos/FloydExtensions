using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator.DataStructure.Properties
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ShortStringPropertyInfo))]
    public class ShortStringPropertyCodeGenerator : PropertyCodeGenerator
    {
        public ShortStringPropertyCodeGenerator(IDslModel dslModel) : base(dslModel)
        {
        }

        protected override string JavaScriptType => "string";
    }
}