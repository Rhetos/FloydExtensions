using System.Collections.Generic;
using System.Linq;

namespace Rhetos.FloydExtensions
{
	public interface ITypeScriptSupportedType
	{
		string PropertyType { get; }
		string TypeScriptType { get; }
	}

	public static class TypeScriptSupportedTypeExtensions
	{
		public static string GetTypeScriptType(this IEnumerable<ITypeScriptSupportedType> types, string propertyType)
		{
			var t = types.FirstOrDefault(x => x.PropertyType == propertyType);
			return t != null ? t.TypeScriptType : "any";
		}
	}
}