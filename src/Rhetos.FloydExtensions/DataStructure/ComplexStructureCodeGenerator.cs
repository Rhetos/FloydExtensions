﻿using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.ComplexEntity;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;
using Rhetos.FloydExtensions.DataStructure.Properties;

namespace Rhetos.FloydExtensions.DataStructure
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(ComplexStructureInfo))]
    public class ComplexStructureCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertIdProprety(info);
        }
    }
}