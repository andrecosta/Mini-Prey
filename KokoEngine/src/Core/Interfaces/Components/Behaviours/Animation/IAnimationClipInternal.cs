using System.Collections.Generic;

namespace KokoEngine
{
    internal interface IAnimationClipInternal : IAnimationClip
    {
        List<ISprite> Sprites { get; set; }

        void CreateSpritesheet();
    }
}