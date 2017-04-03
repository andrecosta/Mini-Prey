using System.Collections.Generic;

namespace KokoEngine
{
    public class AnimationClip : Entity, IAnimationClip
    {
        public List<ISprite> Sprites { get; } = new List<ISprite>();
        public int NumFrames { get; }

        public AnimationClip(ISprite sprite, int numFrames)
        {
            NumFrames = numFrames;

            int frameWidth = sprite.SourceRect.Width / numFrames;

            for (int i = 0; i < numFrames; i++)
            {
                Sprites.Add(new Sprite(sprite.Texture,
                    new Rect(i * frameWidth, sprite.SourceRect.Y, frameWidth, sprite.SourceRect.Height)));
            }
        }
    }
}
