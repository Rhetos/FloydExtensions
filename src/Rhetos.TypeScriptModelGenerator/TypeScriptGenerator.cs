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

namespace Rhetos.TypeScriptModelGenerator
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

        const string detectLineTag = @"\n\s*/\*.*?\*/\s*\r?\n";
        const string detectTag = @"/\*.*?\*/";
        const string detectLastComma = "},\r\n    ];\r\n\r\n";
        const string detectLastComma2 = "},\r\n            ],\r\n";
        const string detectLastComma3 = "],\r\n        };\r\n";
        const string detectLastComma4 = "},\r\n        ];";

        public IEnumerable<string> Dependencies
        {
            get { return null; }
        }

        private static void CompileFileTS()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C cd /d " + Paths.ResourcesFolder + "/AdminGuiCompile/scripts" + " & tsc";
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }
        private static void CopyCompiledFile()
        {
            string sourceFile = Path.Combine(Paths.ResourcesFolder + "/AdminGuiCompile/dist/admingui.js");
            string destinationFile = Path.Combine(Paths.ResourcesFolder + "/AdminGui/js/admingui.js");
            File.Copy(sourceFile, destinationFile, true);
        }

        public void Generate()
        {
            var sw = Stopwatch.StartNew();
            SimpleAssemblySource assemblySource = GenerateSource();

            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, detectLineTag, "\n");
            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, detectTag, "");
            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, detectLastComma, "}\r\n    ];\r\n\r\n");
            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, detectLastComma2, "}\r\n            ],\r\n");
            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, detectLastComma3, "]\r\n        };\r\n");
            assemblySource.GeneratedCode = Regex.Replace(assemblySource.GeneratedCode, detectLastComma4, "}\r\n        ];");

            string sourceFile = Path.Combine(Paths.GeneratedFolder + @"\RhetosModel.ts");
            File.WriteAllText(sourceFile, assemblySource.GeneratedCode);

            //            CompileFileTS();
            //            CopyCompiledFile();

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
