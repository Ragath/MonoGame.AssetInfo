using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.IO;
using System.Linq;

namespace MonoGame.AssetInfo.Pipeline;

/// <summary>
/// Dependency tracking bug workaround.
/// </summary>
[ContentProcessor(DisplayName = "MGCB Processor")]
public class MGCBProcessor : ContentProcessor<AssetInfoContent[], AssetInfoContent[]>
{
    public override AssetInfoContent[] Process(AssetInfoContent[] input, ContentProcessorContext context)
    {
        var matcher = new Matcher(StringComparison.InvariantCultureIgnoreCase);
        matcher.AddIncludePatterns(File.ReadLines(context.SourceIdentity.SourceFilename));
        var files = matcher.GetResultsInFullPath(Path.GetDirectoryName(context.SourceIdentity.SourceFilename)).ToArray();

        foreach (var f in files)
            context.AddDependency(f);

        return input;
    }
}
