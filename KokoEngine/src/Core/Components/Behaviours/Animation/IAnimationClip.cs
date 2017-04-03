using System.Collections.Generic;

namespace KokoEngine
{
    public interface IAnimationClip : IEntity
    {
        int NumFrames { get; }
        List<ISprite> Sprites { get; }
    }
}