using System.ComponentModel.Composition;
using System.Linq;
using Rhetos.Compiler;
using Rhetos.ComplexEntity;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptGeneratorPlugin))]
	[ExportMetadata(MefProvider.Implements, typeof(CreateGetInfo))]
	public class CreateGetCodeGenerator : ITypeScriptGeneratorPlugin
	{
		private readonly IDslModel _dslModel;

		public CreateGetCodeGenerator(IDslModel dslModel)
		{
			_dslModel = dslModel;
		}

		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
		{
			var gettable = (CreateGetInfo)conceptInfo;
			var persistable = _dslModel.FindByType<CreateSaveInfo>()
				.FirstOrDefault(x => x.ComplexStructure == gettable.ComplexStructure);
			var info = gettable.ComplexStructure;

			if (persistable != null)
			{
				codeBuilder.InsertCode($@"
    export const {info.Name}ComplexInfo = createComplexInfo({info.Name}GetFunctionInfo, {info.Name}SaveFunctioInfo);
", ModuleCodeGenerator.MembersTag, info.Module);
			}
			else
			{
				codeBuilder.InsertCode($@"
    export const {info.Name}ComplexGetInfo = createComplexGetInfo({info.Name}GetFunctionInfo);
", ModuleCodeGenerator.MembersTag, info.Module);

			}
		}
	}
}