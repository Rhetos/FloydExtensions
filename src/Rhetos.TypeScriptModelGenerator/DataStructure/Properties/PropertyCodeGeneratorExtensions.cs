using System;
using System.Reflection;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using PropertyInfo = Rhetos.Dsl.DefaultConcepts.PropertyInfo;

namespace Rhetos.TypeScriptModelGenerator.DataStructure.Properties
{
    public static class PropertyCodeGeneratorExtensions
    {
        public static void InsertPropertyCode(this ICodeBuilder codeBuilder, PropertyInfo info, string type, string nameSufix = "", bool isRequired = true)
        {
            var requiredSufix = isRequired ? "" : "?";
            var propertyNameSufix = $"{nameSufix}{requiredSufix}";
            codeBuilder.InsertCode(info.Code(type, propertyNameSufix), DataStructureCodeGenerator.MembersTag, info.DataStructure);

            codeBuilder.InsertCode(info.MetaData(type, nameSufix), DataStructureCodeGenerator.PropertiesMetaDataTag, info.DataStructure);
            
            codeBuilder.InsertCode($"type: '{info.GetType().RhetosKeyword()}'", PropertyCodeGenerator.PropertyMetaDataTag, info);
        }

        private static string Code(this PropertyInfo info, string type, string nameSufix = "")
        {
            return $@"
        {info.Name}{nameSufix}: {type};";
        }

        private static string MetaData(this PropertyInfo info, string type, string nameSufix = "")
        {
            return $@"
            '{info.Name}{nameSufix}': {{
                {PropertyCodeGenerator.PropertyMetaDataTag.Evaluate(info)}
            }}";
        }

        public static void InsertIdProprety(this ICodeBuilder codeBuilder, DataStructureInfo info)
        {
            codeBuilder.InsertCode(@"
        ID?: string;", DataStructureCodeGenerator.MembersTag, info);
        }

        public static string RhetosKeyword(this Type type)
        {
            var attr = type.GetCustomAttribute<ConceptKeywordAttribute>(true);
            return attr?.Keyword;
        }
    }
}