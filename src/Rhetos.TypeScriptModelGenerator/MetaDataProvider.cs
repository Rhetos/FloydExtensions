using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Rhetos.TypeScriptModelGenerator
{
    public class MetadataProvider
    {
        private readonly Dictionary<string, string> _metadata = Load();

        public string GetStructureMetadata(string key)
        {
            if (!_metadata.ContainsKey(key))
                throw new UserException($"Metadata for model '{key}' does not exist.");
            
            return _metadata[key];
        }

        private static Dictionary<string, string> Load()
        {
            var fileName = Path.Combine(Utilities.Paths.GeneratedFolder, "Metadata.json");
            var json = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}