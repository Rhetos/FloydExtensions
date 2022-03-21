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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptGeneratorPlugin))]
	[ExportMetadata(MefProvider.Implements, typeof(EntryValueInfo))]
	public class EntryValueCodeGenerator : ITypeScriptGeneratorPlugin
	{
		private readonly IEnumerable<ITypeScriptSupportedType> _supportedTypes;

		public EntryValueCodeGenerator(IEnumerable<ITypeScriptSupportedType> supportedTypes)
		{
			_supportedTypes = supportedTypes;
		}

		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
		{
			var info = (EntryValueInfo) conceptInfo;
			codeBuilder.InsertCode(Code(info), EntryCodeGenerator.EntryValueTag, info.Entry);
		}

		private string Code(EntryValueInfo info)
		{
			return $"{info.PropertyName}: {GetTypeScriptValue(info)}";
		}

		private string GetTypeScriptValue(EntryValueInfo info)
		{
			var type = _supportedTypes.SingleOrDefault(x => x.DslType.IsInstanceOfType(info.Property));
			if (type == null)
				throw new DslSyntaxException($"Property {info.Property.FullName} is not supported for Hardcoded concept.");

			var stringTypes = new[] {"string"};

			if (stringTypes.Contains(type.TypeScriptType))
				return $"'{info.Value}'";

			return info.Value;
		}
	}
}