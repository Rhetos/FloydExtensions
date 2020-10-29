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
using System.Linq;
using Rhetos.Compiler;
using Rhetos.ComplexEntity.Function;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
	[Export(typeof(ITypeScriptGeneratorPlugin))]
	[ExportMetadata(MefProvider.Implements, typeof(ComplexGetFunctionInfo))]
	public class ComplexGetFunctionCodeGenerator : ITypeScriptGeneratorPlugin
	{
		private readonly IDslModel _dslModel;

		public ComplexGetFunctionCodeGenerator(IDslModel dslModel)
		{
			_dslModel = dslModel;
		}

		public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
		{
			var info = (ComplexGetFunctionInfo)conceptInfo;

			if (_dslModel.FindByType<ComplexSaveFunctionInfo>().Any(x => x.ComplexStructure == info.ComplexStructure))
				return;

			codeBuilder.InsertCode($@"
    export const {info.ComplexStructure.Name}ComplexInfo = createComplexGetInfo({info.ComplexStructure.Name}GetFunctionInfo);
", ModuleCodeGenerator.MembersTag, info.ComplexStructure.Module);
		}
	}
}