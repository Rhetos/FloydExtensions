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

namespace Rhetos.TypeScriptModelGenerator.DataStructure
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(DataStructureInfo))]
    public class DataStructureCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<DataStructureInfo> MembersTag = "TsProperties";
        public static readonly CsTag<DataStructureInfo> ImplementsTag = new CsTag<DataStructureInfo>("TsImplements", TagType.Appendable, "implements {0}", ", {0}");
        public static readonly CsTag<DataStructureInfo> AttributesTag = "TsAttributes";


        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            DataStructureInfo info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertCode(Code(info), ModuleCodeGenerator.Members, info.Module);
            codeBuilder.InsertCode("IRhetosStructure", ImplementsTag, info);
        }

        private static string Code(DataStructureInfo info)
        {
            return $@"
    {AttributesTag.Evaluate(info)} 
    export class {info.Name} {ImplementsTag.Evaluate(info)} {{
        get moduleName(): string {{ return '{info.Module.Name}';}}
        get structureName(): string {{ return '{info.Name}';}}
        {MembersTag.Evaluate(info)}
    }}
";
        }
    }

    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(EntityInfo))]
    public class EntityCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo) conceptInfo;
            codeBuilder.InsertIdProprety(info);
            codeBuilder.InsertCode("IRhetosEntity", DataStructureCodeGenerator.ImplementsTag, info);
        }
    }

    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(BrowseDataStructureInfo))]
    public class BrowseCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertIdProprety(info);
            codeBuilder.InsertCode("IRhetosQueryableStructure", DataStructureCodeGenerator.ImplementsTag, info);
        }
    }

    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(SqlQueryableInfo))]
    public class SqlQueryableCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertIdProprety(info);
            codeBuilder.InsertCode("IRhetosQueryableStructure", DataStructureCodeGenerator.ImplementsTag, info);
        }
    }

    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ComputedInfo))]
    public class ComputedCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertIdProprety(info);
            codeBuilder.InsertCode("IRhetosQueryableStructure", DataStructureCodeGenerator.ImplementsTag, info);
        }
    }

    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(QueryableExtensionInfo))]
    public class QueryableExtensionCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertIdProprety(info);
            codeBuilder.InsertCode("IRhetosQueryableStructure", DataStructureCodeGenerator.ImplementsTag, info);
        }
    }

    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ActionInfo))]
    public class ActionCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertCode("IRhetosAction", DataStructureCodeGenerator.ImplementsTag, info);
        }
    }

    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(TypeScriptFilterInfo))]
    public class FilterCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (TypeScriptFilterInfo)conceptInfo;
            codeBuilder.InsertCode("IRhetosFilterParameter", DataStructureCodeGenerator.ImplementsTag, info.DataStructure);
        }
    }

    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(RowPermissionsReadInfo))]
    public class RowPermissionCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (RowPermissionsReadInfo)conceptInfo;
            codeBuilder.InsertCode(@"@HasReadRowPermissions
    ", DataStructureCodeGenerator.AttributesTag, info.Source);
        }
    }

}