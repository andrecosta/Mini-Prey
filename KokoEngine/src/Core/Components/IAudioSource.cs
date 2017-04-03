namespace KokoEngine
{
    public interface IAudioSource : IComponent
    {
        AudioClip AudioClip { get; set; }
        float Pan { get; set; }
        float Pitch { get; set; }
        float Volume { get; set; }

        void Play(AudioClip clip);
    }
}