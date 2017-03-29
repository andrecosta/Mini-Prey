using System.Collections.Generic;

namespace KokoEngine
{
    public class AssetManager : IAssetManager
    {
        private readonly Dictionary<string, IAsset> _assetMap = new Dictionary<string, IAsset>();

        public void AddAsset<T>(string key, T asset) where T : IAsset
        {
            _assetMap.Add(key, asset);
        }

        public T GetAsset<T>(string key) where T : IAsset
        {
            return (T)_assetMap[key];
        }
    }
}
