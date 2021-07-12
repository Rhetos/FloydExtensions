/*
    Copyright (C) 2014 Omega software d.o.o.

    This file is part of Rhetos.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Reflection;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using PropertyInfo = Rhetos.Dsl.DefaultConcepts.PropertyInfo;

namespace Rhetos.FloydExtensions
{
    public static class PropertyCodeGeneratorExtensions
    {
        public static void InsertPropertyCode(this ICodeBuilder codeBuilder, PropertyInfo info, string type, string nameSufix = "", bool isRequired = true)
        {
            var requiredSufix = isRequired ? "" : "?";
            var propertyNameSufix = $"{nameSufix}{requiredSufix}";
            codeBuilder.InsertCode(info.Code(type, propertyNameSufix), DataStructureCodeGenerator.MembersTag, info.DataStructure);

            codeBuilder.InsertCode(info.MetaData(type, nameSufix), DataStructureCodeGenerator.PropertiesMetaDataTag, info.DataStructure);
            
            codeBuilder.InsertCode($@"\""type\"": \""{info.GetType().RhetosKeyword()}\""", PropertyCodeGenerator.PropertyMetaDataTag, info);
        }

        private static string Code(this PropertyInfo info, string type, string nameSufix = "")
        {
            return $@"
        {info.Name}{nameSufix}: {type};";
        }

        private static string MetaData(this PropertyInfo info, string type, string nameSufix = "")
        {
            return $@"\""{info.Name}{nameSufix}\"": {{ {PropertyCodeGenerator.PropertyMetaDataTag.Evaluate(info)} }}";
        }

        public static void InsertIdProprety(this ICodeBuilder codeBuilder, DataStructureInfo info)
        {
            codeBuilder.InsertCode(@"
        ID: string;", DataStructureCodeGenerator.MembersTag, info);
        }

        public static string RhetosKeyword(this Type type)
        {
            var attr = type.GetCustomAttribute<ConceptKeywordAttribute>(true);
            return attr?.Keyword;
        }
    }
}