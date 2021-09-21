using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptGeneratorPlugin))]
	[ExportMetadata(MefProvider.Implements, typeof(EntryInfo))]
	public class EntryCodeGenerator : ITypeScriptGeneratorPlugin
	{
		public static readonly CsTag<EntryInfo> EntryValueTag = new CsTag<EntryInfo>("TsHardcodedEntryValue", TagType.Appendable, ", {0}", @", {0}");

		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
		{
			var info = (EntryInfo) conceptInfo;
			codeBuilder.InsertCode(IdCode(info), HardcodedCodeGenerator.EntryIdTag, info.HardcodedEntity);
			codeBuilder.InsertCode(ItemCode(info), HardcodedCodeGenerator.EntryItemTag, info.HardcodedEntity);
		}

		private static string IdCode(EntryInfo info)
		{
			return $@"{info.Name} = '{info.GetIdentifier()}'";
		}

		private static string ItemCode(EntryInfo info)
		{
			return $@"{{ ID: '{info.GetIdentifier()}', Name: '{info.Name}'{EntryValueTag.Evaluate(info)} }}";
		}
	}
}