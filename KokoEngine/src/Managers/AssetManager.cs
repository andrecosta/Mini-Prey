using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KokoEngine
{
    public class AssetManager
    {
        // Singleton
        public static AssetManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                _instance = new AssetManager();
                return _instance;
            }
        }

        private static AssetManager _instance;

        private readonly Dictionary<string, IAsset> _assetMap = new Dictionary<string, IAsset>();

        private AssetManager() { }

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
