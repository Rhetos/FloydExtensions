using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Rhetos.Compiler;
using Rhetos.Extensibility;
using Rhetos.Logging;
using Rhetos.Utilities;
using ICodeGenerator = Rhetos.Compiler.ICodeGenerator;

namespace Rhetos.FloydExtensions
{
    [Export(typeof(IGenerator))]
    public class TypeScriptGenerator : IGenerator
    {
        private readonly IPluginsContainer<ITypeScriptGeneratorPlugin> _plugins;
        private readonly ICodeGenerator _codeGenerator;
        private readonly ILogger _performanceLogger;

        public TypeScriptGenerator(
            IPluginsContainer<ITypeScriptGeneratorPlugin> plugins,
            ICodeGenerator codeGenerator,
            ILogProvider logProvider
        )
        {
            _plugins = plugins;
            _codeGenerator = codeGenerator;
            _performanceLogger = logProvider.GetLogger("Performance");
        }

        const string DetectLineTag = @"\n\s*/\*.*?\*/\s*\r?\n";
        const string DetectTag = @"/\*.*?\*/";
        const string DetectLastComma = "},\r\n    ];\r\n\r\n";
        const string DetectLastComma2 = "},\r\n            ],\r\n";
        const string DetectLastComma3 = "],\r\n        };\r\n";
        const string DetectLastComma4 = "},\r\n        ];";

        public IEnumerable<string> Dependencies => null;

        public void Generate()
        {
            var sw = Stopwatch.StartNew();
            SimpleAssemblySource assemblySource = GenerateSource();

            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, DetectLineTag, "\n");
            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, DetectTag, "");
            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, DetectLastComma, "}\r\n    ];\r\n\r\n");
            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, DetectLastComma2, "}\r\n            ],\r\n");
            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, DetectLastComma3, "]\r\n        };\r\n");
            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, DetectLastComma4, "}\r\n        ];");

            var source = assemblySource.GeneratedCode.Split(new[] {TypeScriptGeneratorInitialCodeGenerator.FileSplitTag}, StringSplitOptions.None);

            string sourceFile = Path.Combine(Paths.GeneratedFolder + @"\rhetos-model.ts");
            var typeScript = $@"/* tslint:disable:no-empty-interface class-name no-namespace */

import {{ createStructureInfo, createFunctionInfo, createComplexInfo, createComplexGetInfo }} from '@ngx-floyd/rhetos';

{source[0]}
";
            File.WriteAllText(sourceFile, typeScript);

            string jsonFile = Path.Combine(Paths.GeneratedFolder + @"\Metadata.json");
            var json = source.Length > 1 ? source[1] : "";
            File.WriteAllText(jsonFile, json);

            _performanceLogger.Write(sw, "TypeScriptGenerator.Generate");
        }

        private SimpleAssemblySource GenerateSource()
        {
            IAssemblySource generatedSource = _codeGenerator.ExecutePlugins(_plugins, "/*", "*/", null);
            SimpleAssemblySource assemblySource = new SimpleAssemblySource
            {
                GeneratedCode = generatedSource.GeneratedCode,
                RegisteredReferences = generatedSource.RegisteredReferences
            };
            return assemblySource;
        }
    }
}
