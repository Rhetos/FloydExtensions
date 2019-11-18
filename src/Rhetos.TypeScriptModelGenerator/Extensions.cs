using Rhetos.Compiler;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.TypeScriptModelGenerator.DataStructure;

namespace Rhetos.TypeScriptModelGenerator
{
    internal static class Extensions
    {
        public static void InsertPropertyCode(this ICodeBuilder codeBuilder, PropertyInfo info, string type)
        {
            codeBuilder.InsertCode(info.Code(type), DataStructureCodeGenerator.Members, info.DataStructure);
        }

        private static string Code(this PropertyInfo info, string type)
        {
            return $@"
        {info.Name}: {type};";
        }
    }
}