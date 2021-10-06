using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptGeneratorPlugin))]
	[ExportMetadata(MefProvider.Implements, typeof(EntryValueInfo))]
	public class EntryValueCodeGenerator : ITypeScriptGeneratorPlugin
	{
		private readonly IEnumerable<ITypeScriptSupportedType> _supportedTypes;

		public EntryValueCodeGenerator(IEnumerable<ITypeScriptSupportedType> supportedTypes)
		{
			_supportedTypes = supportedTypes;
		}

		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
		{
			var info = (EntryValueInfo) conceptInfo;
			codeBuilder.InsertCode(Code(info), EntryCodeGenerator.EntryValueTag, info.Entry);
		}

		private string Code(EntryValueInfo info)
		{
			return $"{info.PropertyName}: {GetTypeScriptValue(info)}";
		}

		private string GetTypeScriptValue(EntryValueInfo info)
		{
			var type = _supportedTypes.SingleOrDefault(x => x.DslType.IsInstanceOfType(info.Property));
			if (type == null)
				throw new DslSyntaxException($"Property {info.Property.FullName} is not supported for Hardcoded concept.");

			var stringTypes = new[] {"string"};

			if (stringTypes.Contains(type.TypeScriptType))
				return $"'{info.Value}'";

			return info.Value;
		}
	}
}