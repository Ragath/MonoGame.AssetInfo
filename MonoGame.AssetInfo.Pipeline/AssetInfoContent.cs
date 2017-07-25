using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace MonoGame.AssetInfo.Pipeline
{
    [ContentSerializerRuntimeType("MonoGame.AssetInfo.AssetInfo, MonoGame.AssetInfo")]
    public class AssetInfoContent
    {
        public string SourcePath { get; set; }
        public string OutputPath { get; set; }

        public string Importer { get; set; }
        public string Processor { get; set; }
        
        public IReadOnlyDictionary<string, string> ProcessorParameters { get; set; } = new Dictionary<string, string>();
    }
}
