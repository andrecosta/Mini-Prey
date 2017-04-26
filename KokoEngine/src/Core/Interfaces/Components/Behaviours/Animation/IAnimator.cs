namespace KokoEngine
{
    /// <summary>
    /// Controls the animation of an <see cref="IAnimationClip"/>.
    /// </summary>
    public interface IAnimator : IBehaviour
    {
        /// <summary>
        /// Returns the current state of the animation playback. (Read Only)
        /// </summary>
        bool IsPlaying { get; }
        
        /// <summary>
        /// Determines if the animation is loopable.
        /// </summary>
        bool Looping { get; set; }

        /// <summary>
        /// Controls the speed (in seconds) of the animation.
        /// </summary>
        float Speed { get; set; }

        /// <summary>
        /// Adds a new animation to be controlled.
        /// </summary>
        /// <param name="key">The name by which the animation clip is accessed.</param>
        /// <param name="clip">The animation clip to be added.</param>
        void AddAnimation(string key, IAnimationClip clip);

        /// <summary>
        /// Plays an animation clip.
        /// </summary>
        /// <param name="animationName">The name (key) of the animation clip.</param>
        void Play(string animationName);

        /// <summary>
        /// Plays an animation clip.
        /// </summary>
        /// <param name="animationName">The name (key) of the animation clip.</param>
        /// <param name="speed">The speed of playback.</param>
        void Play(string animationName, float speed);

        /// <summary>
        /// Stops the playback of any currently active animation clip.
        /// </summary>
        void Stop();
    }
}