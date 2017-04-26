using System.Collections.Generic;

namespace KokoEngine
{
    /// <summary>
    /// Stores a sprite based animation.
    /// It's used by <see cref="IAnimator"/> to play animations.
    /// </summary>
    public interface IAnimationClip : IEntity
    {
        /// <summary>
        /// Number of frames of this animation clip.
        /// </summary>
        int NumFrames { get; }

        /// <summary>
        /// The sprite used as a base for this animation clip.
        /// </summary>
        ISprite Sprite { get; }
    }
}