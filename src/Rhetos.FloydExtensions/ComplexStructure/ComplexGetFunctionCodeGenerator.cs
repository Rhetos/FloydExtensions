using System.ComponentModel.Composition;
using System.Linq;
using Rhetos.Compiler;
using Rhetos.ComplexEntity.Function;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptGeneratorPlugin))]
	[ExportMetadata(MefProvider.Implements, typeof(ComplexGetFunctionInfo))]
	public class ComplexGetFunctionCodeGenerator : ITypeScriptGeneratorPlugin
	{
		private readonly IDslModel _dslModel;

		public ComplexGetFunctionCodeGenerator(IDslModel dslModel)
		{
			_dslModel = dslModel;
		}

		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
		{
			var info = (ComplexGetFunctionInfo)conceptInfo;

			if (_dslModel.FindByType<ComplexSaveFunctionInfo>().Any(x => x.ComplexStructure == info.ComplexStructure))
				return;

			codeBuilder.InsertCode($@"
    export const {info.ComplexStructure.Name}ComplexInfo = createComplexGetInfo({info.ComplexStructure.Name}GetFunctionInfo);
", ModuleCodeGenerator.MembersTag, info.ComplexStructure.Module);
		}
	}
}