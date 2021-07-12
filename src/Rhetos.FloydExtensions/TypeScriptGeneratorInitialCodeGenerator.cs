﻿/*
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
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(InitializationConcept))]
    public class TypeScriptGeneratorInitialCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<TsBodyInfo> TsMembersTag = "TsMembers";
        public static readonly CsTag<TsBodyInfo> StructureMetaDataTag = new CsTag<TsBodyInfo>("StructureMetaDataTag", TagType.Appendable, "{0}", @", 
    {0}");

        public const string FileSplitTag = "--- File Split ---";
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            codeBuilder.InsertCode(_codeSnippet);
        }
        private readonly string _codeSnippet =
$@"
{TsMembersTag.Evaluate(new TsBodyInfo())}
{FileSplitTag}

{{{StructureMetaDataTag.Evaluate(new TsBodyInfo())}
}}
";
    }

    public class TsBodyInfo : IConceptInfo
    {
        [ConceptKey]
        public string Name { get; set; } = "TypeScriptBody";
    }
}
