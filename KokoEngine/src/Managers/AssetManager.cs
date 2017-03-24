using System.Collections.Generic;

namespace KokoEngine
{
    public class AssetManager
    {
        // Singleton
        private static AssetManager _instance;
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
