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
using Rhetos.ComplexEntity.ComplexParameter;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ObjectPropertyDslModelInfo))]
    public class ObjectDslPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ObjectPropertyDslModelInfo) conceptInfo;
            codeBuilder.InsertCode($@"
        {info.Name}?: {info.PropertyStructure.Module.Name}.{info.PropertyStructure.Name};", DataStructureCodeGenerator.MembersTag, info.DataStructure);

            codeBuilder.InsertCode($@"\""{info.Name}\"": {{ \""type\"": \""Object\"",  \""keyOfComplexMember\"": \""{info.PropertyStructure.Module.Name}/{info.PropertyStructure.Name}\""}}", DataStructureCodeGenerator.NavigationalPropertiesMetaDataTag, info.DataStructure);
        }
    }
    
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ObjectPropertyInfo))]
    public class ObjectPropertyCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (ObjectPropertyInfo) conceptInfo;
            codeBuilder.InsertCode($@"
        {info.Name}?: {info.DataStructure.Module.Name}.{info.DataStructure.Name};", DataStructureCodeGenerator.MembersTag, info.DataStructure);

            codeBuilder.InsertCode($@"\""{info.Name}\"": {{ \""type\"": \""Object\"",  \""keyOfComplexMember\"": \""{info.DataStructure.Module.Name}/{info.DataStructure.Name}\""}}", DataStructureCodeGenerator.NavigationalPropertiesMetaDataTag, info.DataStructure);
        }
    }
}