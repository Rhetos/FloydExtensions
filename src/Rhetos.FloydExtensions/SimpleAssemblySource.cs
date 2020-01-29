﻿using System.Collections.Generic;
using Rhetos.Compiler;

namespace Rhetos.FloydExtensions
{
    class SimpleAssemblySource : IAssemblySource
    {
        public string GeneratedCode { get; set; }
        public IEnumerable<string> RegisteredReferences { get; set; }
    }
}