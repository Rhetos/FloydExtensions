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
using System.Diagnostics;
using System.Security;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptGeneratorPlugin))]
	[ExportMetadata(MefProvider.Implements, typeof(HardcodedEntityInfo))]
	public class HardcodedCodeGenerator : ITypeScriptGeneratorPlugin
	{
		public static readonly CsTag<HardcodedEntityInfo> EntryIdTag = new CsTag<HardcodedEntityInfo>("TsHardcodedEntryId", TagType.Appendable, "{0}", @",
		{0}");
		public static readonly CsTag<HardcodedEntityInfo> EntryItemTag = new CsTag<HardcodedEntityInfo>("EntryItemTag", TagType.Appendable, "{0}", @",
		{0}");

		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
		{
			var info = (HardcodedEntityInfo) conceptInfo;
			codeBuilder.InsertCode(Code(info), ModuleCodeGenerator.MembersTag, info.Module);
		}

		private static string Code(HardcodedEntityInfo info)
		{
			return $@"
    export const {info.Name}Ids = {{
		{EntryIdTag.Evaluate(info)}
	}};

    export const {info.Name}Items: {info.Module.Name}.{info.Name}[] = [
		{EntryItemTag.Evaluate(info)}
	];
";
		}
	}
}