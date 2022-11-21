using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Security;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptGeneratorPlugin))]
	[ExportMetadata(MefProvider.Implements, typeof(HardcodedEntityInfo))]
	public class HardcodedCodeGenerator : ITypeScriptGeneratorPlugin
	{
		public static readonly CsTag<HardcodedEntityInfo> EntryIdTag = new CsTag<HardcodedEntityInfo>("TsHardcodedEntryId", TagType.Appendable, "{0}", @",
		{0}");
		public static readonly CsTag<HardcodedEntityInfo> EntryItemTag = new CsTag<HardcodedEntityInfo>("EntryItemTag", TagType.Appendable, "{0}", @",
		{0}");

		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
		{
			var info = (HardcodedEntityInfo) conceptInfo;
			codeBuilder.InsertCode(Code(info), ModuleCodeGenerator.MembersTag, info.Module);
		}

		private static string Code(HardcodedEntityInfo info)
		{
			return $@"
    export const {info.Name}Ids = {{
		{EntryIdTag.Evaluate(info)}
	}};

    export const {info.Name}Items: {info.Module.Name}.{info.Name}[] = [
		{EntryItemTag.Evaluate(info)}
	];
";
		}
	}
}