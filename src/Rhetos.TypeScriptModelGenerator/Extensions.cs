using System;
using System.Reflection;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.TypeScriptModelGenerator.DataStructure;
using Rhetos.TypeScriptModelGenerator.DataStructure.Properties;
using PropertyInfo = Rhetos.Dsl.DefaultConcepts.PropertyInfo;

namespace Rhetos.TypeScriptModelGenerator
{
    internal static class Extensions
    {
        public static void InsertPropertyCode(this ICodeBuilder codeBuilder, PropertyInfo info, string type, string nameSufix = "")
        {
            codeBuilder.InsertCode(info.Code(type, nameSufix), DataStructureCodeGenerator.MembersTag, info.DataStructure);
        }

        private static string Code(this PropertyInfo info, string type, string nameSufix = "")
        {
            return $@"
        {ShortStringPropertyCodeGenerator.AttributesTag.Evaluate(info)}
        @RhetosType(RhetosType.{info.GetType().RhetosKeyword()})
        {info.Name}{nameSufix}: {type};";
        }

        public static void InsertIdProprety(this ICodeBuilder codeBuilder, DataStructureInfo info)
        {
            codeBuilder.InsertCode(@"
        ID: string;", DataStructureCodeGenerator.MembersTag, info);
        }

        public static string RhetosKeyword(this Type type)
        {
            var attr = type.GetCustomAttribute<ConceptKeywordAttribute>(false);
            return attr?.Keyword;
        }
    }
}