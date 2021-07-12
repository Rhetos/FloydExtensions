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

using System.Linq;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace Rhetos.FloydExtensions
{
    public abstract class PropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<PropertyInfo> PropertyMetaDataTag = new CsTag<PropertyInfo>("TsPropertyMetaData", TagType.Appendable, "{0}", @", {0}");

        protected readonly IDslModel DslModel;

        public abstract string TypeScriptType { get; }

        protected virtual string NameSufix => string.Empty;

        protected PropertyCodeGenerator(IDslModel dslModel)
        {
            DslModel = dslModel;
        }

        public virtual void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var isRequired = DslModel.FindByType<RequiredPropertyInfo>().Any(x => x.Property == conceptInfo);
            codeBuilder.InsertPropertyCode((PropertyInfo)conceptInfo, TypeScriptType, NameSufix, isRequired);
        }
    }
}