﻿using System.ComponentModel.Composition;
using Rhetos.Compiler;
using Rhetos.Dsl;
using Rhetos.Extensibility;

namespace Rhetos.TypeScriptModelGenerator
{
    [Export(typeof(ITypeScriptGeneratorPlugin))]
    [ExportMetadata(MefProvider.Implements, typeof(InitializationConcept))]
    public class TypeScriptGeneratorInitialCodeGenerator : ITypeScriptGeneratorPlugin
    {
        public static readonly CsTag<TsBodyInfo> Members = "TsMembers";

        public void GenerateCode(IConceptInfo conceptInfo, ICodeBuilder codeBuilder)
        {
            codeBuilder.InsertCode(_codeSnippet);
        }
        private readonly string _codeSnippet =
            $@"
import {{ StructureMetadataMap }} from '@floyd-ng/rhetos';
{Members.Evaluate(new TsBodyInfo())}
";
    }

    public class TsBodyInfo : IConceptInfo
    {
        [ConceptKey]
        public string Name { get; set; } = "TypeScriptBody";
    }
}
