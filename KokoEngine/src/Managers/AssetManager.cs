using System;
using System.Collections.Generic;
using System.IO;

namespace KokoEngine
{
    public class AssetManager : IAssetManager
    {
        public Dictionary<string, IAsset> AssetMap { get; } = new Dictionary<string, IAsset>();

        public T LoadAsset<T>(string filename) where T : IAsset, new()
        {
            T asset = new T();

            // Cast to internal to set name
            IAssetInternal assetInternal = asset as IAssetInternal;
            assetInternal?.SetName(filename);

            string key = asset.Name;

            return LoadAsset(key, asset);
        }

        public T LoadAsset<T>(string key, T asset) where T : IAsset
        {
            AssetMap.Add(key, asset);

            return asset;
        }

        public T GetAsset<T>(string key) where T : IAsset
        {
            return (T)AssetMap[key];
        }
    }
}
