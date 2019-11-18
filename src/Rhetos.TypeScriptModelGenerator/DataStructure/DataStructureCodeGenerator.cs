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
        public static readonly CsTag<DataStructureInfo> Members = "Properties";


        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            DataStructureInfo info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertCode(Code(info), ModuleCodeGenerator.Members, info.Module);
        }

        public static bool IsEntityType(DataStructureInfo conceptInfo)
        {
            return conceptInfo is IOrmDataStructure
                || conceptInfo is BrowseDataStructureInfo
                || conceptInfo is QueryableExtensionInfo
                || conceptInfo is ComputedInfo;
        }

        private static string Code(DataStructureInfo info)
        {
            return $@"
    export class {info.Name} extends RhetosDataStructure {{
        get moduleName(): string {{ return '{info.Module.Name}';}}
        {Members.Evaluate(info)}
    }}
";
        }

    }
}