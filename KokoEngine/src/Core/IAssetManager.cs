namespace KokoEngine
{
    /// <summary>
    /// Manager for storing and retrieving game assets.
    /// </summary>
    public interface IAssetManager
    {
        /// <summary>
        /// Stores an asset of type T.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="key">The key by which the asset will be referenced.</param>
        /// <param name="asset">The raw asset object.</param>
        void AddAsset<T>(string key, T asset) where T : IAsset;

        /// <summary>
        /// Returns an asset of type T.
        /// </summary>
        /// <typeparam name="T">The type of the asset to be returned.</typeparam>
        /// <param name="key">The key of the asset to be returned.</param>
        T GetAsset<T>(string key) where T : IAsset;
    }
}