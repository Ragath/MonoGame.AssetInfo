using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace MonoGame.AssetInfo
{
    public static class ContentManagerExtensions
    {
        public static T Load<T>(this ContentManager content, AssetInfo asset) => content.Load<T>(asset.OutputPath);
        public static IReadOnlyList<AssetInfo> LoadMGCB(this ContentManager content, string assetName) => content.Load<AssetInfo[]>(assetName);
    }
}
