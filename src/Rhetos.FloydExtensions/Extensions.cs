using Rhetos.Compiler;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.FloydExtensions.DataStructure;

namespace Rhetos.FloydExtensions
{
    internal static class Extensions
    {
        public static void InsertServiceType(this ICodeBuilder codeBuilder, DataStructureInfo info, string serviceType)
        {
            codeBuilder.InsertCode($@"\""serviceType\"": \""{serviceType}\""", DataStructureCodeGenerator.StructureMetaDataTag, info);
        }
    }
}