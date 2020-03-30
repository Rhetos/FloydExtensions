using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Rhetos.FloydExtensions
{
    public class MetadataProvider
    {
		private static readonly Lazy<string> Metadata = new Lazy<string>(() =>
		{
			var fileName = Path.Combine(Utilities.Paths.GeneratedFolder, "Metadata.json");
			return File.ReadAllText(fileName);
		});

        public string GetStructureMetadata()
        {
            return Metadata.Value;
        }
    }
}