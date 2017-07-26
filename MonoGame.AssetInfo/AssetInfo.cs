using System.Collections.Generic;

namespace MonoGame.AssetInfo
{
    public sealed class AssetInfo
    {
        public string SourcePath { get; private set; }
        public string OutputPath { get; private set; }

        public string Importer { get; private set; }
        public string Processor { get; private set; }

        public IReadOnlyDictionary<string, string> ProcessorParameters { get; private set; }

        public bool IsXNB { get; private set; }
    }
}
