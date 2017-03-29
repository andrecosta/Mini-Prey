namespace KokoEngine
{
    public interface IAssetManager
    {
        void AddAsset<T>(string key, T asset) where T : IAsset;
        T GetAsset<T>(string key) where T : IAsset;
    }
}