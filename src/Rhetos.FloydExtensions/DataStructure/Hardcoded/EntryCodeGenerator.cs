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
	[ExportMetadata(MefProvider.Implements, typeof(EntryInfo))]
	public class EntryCodeGenerator : ITypeScriptGeneratorPlugin
	{
		public static readonly CsTag<EntryInfo> EntryValueTag = new CsTag<EntryInfo>("TsHardcodedEntryValue", TagType.Appendable, ", {0}", @", {0}");

		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
		{
			var info = (EntryInfo) conceptInfo;
			codeBuilder.InsertCode(IdCode(info), HardcodedCodeGenerator.EntryIdTag, info.HardcodedEntity);
			codeBuilder.InsertCode(ItemCode(info), HardcodedCodeGenerator.EntryItemTag, info.HardcodedEntity);
		}

		private static string IdCode(EntryInfo info)
		{
			return $@"{info.Name}: '{info.GetIdentifier()}'";
		}

		private static string ItemCode(EntryInfo info)
		{
			return $@"{{ ID: '{info.GetIdentifier()}', Name: '{info.Name}'{EntryValueTag.Evaluate(info)} }}";
		}
	}
}