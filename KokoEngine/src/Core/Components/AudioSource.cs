namespace KokoEngine
{
    public class AudioSource : Component, IAudioSource
    {
        public AudioClip AudioClip { get; set; }
        public float Volume { get; set; } = 0.5f;
        public float Pitch { get; set; } = 0;
        public float Pan { get; set; } = 0;

        public void Play(AudioClip clip)
        {
            AudioClip = clip;
        }
    }
}
