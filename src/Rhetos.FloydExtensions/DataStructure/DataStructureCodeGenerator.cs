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

using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(DataStructureInfo))]
    public class DataStructureCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<DataStructureInfo> MembersTag = "TsProperties";
        public static readonly CsTag<DataStructureInfo> ImplementsTag = new CsTag<DataStructureInfo>("TsImplements", TagType.Appendable, "extends {0}", ", {0}");
        public static readonly CsTag<DataStructureInfo> StructureMetaDataTag = new CsTag<DataStructureInfo>("TsStructureMetaDataTag", TagType.Appendable, "{0}", @", {0}");
        public static readonly CsTag<DataStructureInfo> PropertiesMetaDataTag = new CsTag<DataStructureInfo>("TsPropertiesMetaDataTag", TagType.Appendable, "{0}", @", {0}");
        public static readonly CsTag<DataStructureInfo> NavigationalPropertiesMetaDataTag = new CsTag<DataStructureInfo>("TsNavigationalPropertiesMetaDataTag", TagType.Appendable, "{0}", @", {0}");

		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            DataStructureInfo info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertCode(Code(info), ModuleCodeGenerator.MembersTag, info.Module);
            codeBuilder.InsertCode(MetaData(info), TypeScriptGeneratorInitialCodeGenerator.StructureMetaDataTag, new TsBodyInfo());
            codeBuilder.InsertCode($@"\""key\"": \""{info.Module.Name}/{info.Name}\""", StructureMetaDataTag, info);
        }

        private static string Code(DataStructureInfo info)
        {
            return $@"
    export const {info.Name}Info = createStructureInfo<{info.Module.Name}.{info.Name}>('{info.Module.Name}/{info.Name}');
    export interface {info.Name} {ImplementsTag.Evaluate(info)}{{{MembersTag.Evaluate(info)}
    }}
";
        }

        private static string MetaData(DataStructureInfo info)
        {
            return $@"""{info.Module.Name}/{info.Name}"": ""{{ {StructureMetaDataTag.Evaluate(info)}, \""properties\"": {{ {PropertiesMetaDataTag.Evaluate(info)} }}, \""navigationalProperties\"": {{ {NavigationalPropertiesMetaDataTag.Evaluate(info)} }} }}""";
        }
    }
}