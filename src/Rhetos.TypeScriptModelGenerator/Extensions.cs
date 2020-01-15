using Rhetos.Compiler;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.TypeScriptModelGenerator.DataStructure;

namespace Rhetos.TypeScriptModelGenerator
{
    internal static class Extensions
    {
        public static void InsertServiceType(this ICodeBuilder codeBuilder, DataStructureInfo info, string serviceType)
        {
            codeBuilder.InsertCode($@"\""serviceType\"": \""{serviceType}\""", DataStructureCodeGenerator.StructureMetaDataTag, info);
        }
    }
}