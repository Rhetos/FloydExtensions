using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.ComplexEntity.Function;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptGeneratorPlugin))]
	[ExportMetadata(MefProvider.Implements, typeof(ComplexSaveFunctionInfo))]
	public class ComplexSaveFunctionCodeGenerator : ITypeScriptGeneratorPlugin
	{
		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
		{
			var info = (ComplexSaveFunctionInfo)conceptInfo;

			codeBuilder.InsertCode($@"
    export const {info.ComplexStructure.Name}ComplexInfo = createComplexInfo({info.ComplexStructure.Name}GetFunctionInfo, {info.ComplexStructure.Name}SaveFunctionInfo);
", ModuleCodeGenerator.MembersTag, info.ComplexStructure.Module);
		}
	}
}