using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using Microsoft.Extensions.FileSystemGlobbing;

namespace MonoGame.AssetInfo.Pipeline
{
    [ContentImporter(".info", DisplayName = "MGCB Importer", DefaultProcessor = "PassThroughProcessor")]
    public class MGCBImporter : ContentImporter<AssetInfoContent[]>
    {
        public override AssetInfoContent[] Import(string filename, ContentImporterContext context)
        {
            var matcher = new Matcher(StringComparison.InvariantCultureIgnoreCase);
            matcher.AddIncludePatterns(File.ReadLines(filename));

            return matcher
                .GetResultsInFullPath(Path.GetDirectoryName(filename))
                .SelectMany(file => ParseMGCB(File.ReadLines(file)))
                .ToArray();
            //return ParseMGCB(File.ReadLines(filename)).ToArray();
        }

        static IEnumerable<AssetInfoContent> ParseMGCB(IEnumerable<string> lines)
        {
            const string patternBegin = "#begin ";
            const string patternImporter = "/importer:";
            const string patternProcessor = "/processor:";
            const string patternProcessorParam = "/processorParam:";
            const string patternBuild = "/build:";
            const string patternCopy = "/copy:";


            AssetInfoContent asset = null;
            foreach (var line in lines)
                if (line.StartsWith(patternBegin))
                {
                    if (asset != null)
                        yield return asset;

                    asset = new AssetInfoContent
                    {
                        SourcePath = line.Substring(patternBegin.Length)
                    };
                }
                else if (line.StartsWith(patternImporter))
                    asset.Importer = line.Substring(patternImporter.Length);
                else if (line.StartsWith(patternProcessor))
                    asset.Processor = line.Substring(patternProcessor.Length);
                else if (line.StartsWith(patternProcessorParam))
                {
                    var parts = line.Substring(patternProcessorParam.Length).Split(new[] { '=' }, 2);
                    ((Dictionary<string, string>)asset.ProcessorParameters)[parts[0]] = parts[1];
                }
                else if (line.StartsWith(patternBuild))
                    asset.OutputPath = Path.ChangeExtension(line.Substring(patternBuild.Length), null);
                else if (line.StartsWith(patternCopy))
                {
                    asset.OutputPath = line.Substring(patternCopy.Length);
                    if (".xnb".Equals(Path.GetExtension(asset.OutputPath), StringComparison.InvariantCultureIgnoreCase))
                        asset.OutputPath = Path.ChangeExtension(asset.OutputPath, null);
                    else
                        asset.IsXNB = false;
                }

            if (asset != null)
                yield return asset;
        }
    }
}
