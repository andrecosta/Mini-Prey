namespace KokoEngine
{
    internal interface IAssetManagerInternal : IAssetManager
    {
        void Initialize();

        void PlaySound(IAudioSource au, AudioClip clip);
    }
}