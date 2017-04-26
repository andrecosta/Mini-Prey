using System;
using System.Collections.Generic;
using System.IO;

namespace KokoEngine
{
    public class AssetManager : IAssetManagerInternal
    {
        public Action<Texture2D> LoadTextureHandler { get; set; }
        public Action<AudioClip> LoadAudioClipHandler { get; set; }
        public Action<Font> LoadFontHandler { get; set; }

        public string RootDirectory { get; set; }

        private readonly Dictionary<string, IAsset> _assetMap = new Dictionary<string, IAsset>();

        public T AddAsset<T>(string filename) where T : IAsset, new()
        {
            T asset = new T();

            // Cast to internal to set name
            IAssetInternal assetInternal = asset as IAssetInternal;
            assetInternal?.SetName(filename);

            string key = asset.Name;

            return AddAsset(key, asset);
        }

        public T AddAsset<T>(string key, T asset) where T : IAsset
        {
            _assetMap.Add(key, asset);

            return asset;
        }

        public T GetAsset<T>(string key) where T : IAsset
        {
            return (T)_assetMap[key];
        }

        void IAssetManagerInternal.Initialize()
        {
            // Load assets
            foreach (var assetEntry in _assetMap)
            {
                IAsset asset = assetEntry.Value;

                if (asset is Texture2D)
                    LoadTextureHandler?.Invoke(asset as Texture2D);
                else if (asset is AudioClip)
                    LoadAudioClipHandler?.Invoke(asset as AudioClip);
                else if (asset is Font)
                    LoadFontHandler?.Invoke(asset as Font);
            }
        }
    }
}
