﻿using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;
using Rhetos.TypeScriptModelGenerator.DataStructure.Properties;

namespace Rhetos.TypeScriptModelGenerator.DataStructure
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(BrowseDataStructureInfo))]
    public class BrowseCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (DataStructureInfo)conceptInfo;
            codeBuilder.InsertIdProprety(info);
            codeBuilder.InsertServiceType(info, "Queryable");
        }
    }
}