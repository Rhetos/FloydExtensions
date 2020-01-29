﻿using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using Rhetos.Extensibility;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(MinLengthInfo))]
    public class MinLengthTagCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            var info = (MinLengthInfo)conceptInfo;
            codeBuilder.InsertCode($@"\""minLength\"": {info.Length}", PropertyCodeGenerator.PropertyMetaDataTag, info.Property);
        }
    }
}