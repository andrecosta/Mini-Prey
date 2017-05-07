namespace KokoEngine
{
    /// <summary>
    /// Provides sound playback capability to a GameObject.
    /// It stores an audio clip and its playback properties.
    /// </summary>
    public interface IAudioSource : IComponent
    {
        /// <summary>
        /// The volume of the audio playback. [0 to 1]
        /// </summary>
        float Volume { get; set; }

        /// <summary>
        /// The pitch value of the audio playback. [0 to 1]
        /// </summary>
        float Pitch { get; set; }

        /// <summary>
        /// The pan value of the audio playback. [-1 to 1]
        /// </summary>
        float Pan { get; set; }

        /// <summary>
        /// Plays an audio clip.
        /// </summary>
        /// <param name="clip">The clip to be played.</param>
        void Play(AudioClip clip);
    }
}