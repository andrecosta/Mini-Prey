using System;
using System.Collections.Generic;

namespace KokoEngine
{
    /// <summary>
    /// Manager for storing and retrieving game assets.
    /// </summary>
    public interface IAssetManager
    {
        Dictionary<string, IAsset> AssetMap { get; }

        /// <summary>
        /// Loads an asset of type T.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="filename">The filename of the asset to load. This will be used as the key for retrieval.</param>
        T LoadAsset<T>(string filename) where T : IAsset, new();

        /// <summary>
        /// Loads a pre-created asset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        T LoadAsset<T>(string key, T asset) where T : IAsset;

        /// <summary>
        /// Returns an asset of type T.
        /// </summary>
        /// <typeparam name="T">The type of the asset to be returned.</typeparam>
        /// <param name="key">The key of the asset to be returned.</param>
        T GetAsset<T>(string key) where T : IAsset;
    }
}