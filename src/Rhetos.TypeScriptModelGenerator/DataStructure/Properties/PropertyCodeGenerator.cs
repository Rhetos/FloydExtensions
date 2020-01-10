using System;
using System.Reflection;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using PropertyInfo = Rhetos.Dsl.DefaultConcepts.PropertyInfo;

namespace Rhetos.TypeScriptModelGenerator.DataStructure.Properties
{
    public static class PropertyCodeGenerator
    {
        public static readonly CsTag<PropertyInfo> PropertyMetaDataTag = new CsTag<PropertyInfo>("TsPropertyMetaData", TagType.Appendable, "{0}", @", 
                    {0}");

        public static void InsertPropertyCode(this ICodeBuilder codeBuilder, PropertyInfo info, string type, string nameSufix = "")
        {
            codeBuilder.InsertCode(info.Code(type, nameSufix), DataStructureCodeGenerator.MembersTag, info.DataStructure);
            codeBuilder.InsertCode(info.MetaData(type, nameSufix), DataStructureCodeGenerator.PropertiesMetaDataTag, info.DataStructure);
            var rhetosType = info.GetType().RhetosKeyword();
            codeBuilder.InsertCode($"type: '{rhetosType}'", PropertyMetaDataTag, info);
        }

        private static string Code(this PropertyInfo info, string type, string nameSufix = "")
        {
            return $@"
        {info.Name}{nameSufix}: {type};";
        }

        private static string MetaData(this PropertyInfo info, string type, string nameSufix = "")
        {
            return $@"
                {info.Name}{nameSufix}: {{
                    {PropertyMetaDataTag.Evaluate(info)}
                }}";
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