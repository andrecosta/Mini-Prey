using System.Collections.Generic;

namespace KokoEngine
{
    public class AnimationClip : Entity, IAnimationClipInternal
    {
        public ISprite Sprite { get; }
        public int NumFrames { get; }

        List<ISprite> IAnimationClipInternal.Sprites { get; set; } = new List<ISprite>();

        public AnimationClip(ISprite sprite, int numFrames)
        {
            Sprite = sprite;
            NumFrames = numFrames;
        }
        
        void IAnimationClipInternal.CreateSpritesheet()
        {
            int frameWidth = Sprite.SourceRect.Width / NumFrames;

            for (int i = 0; i < NumFrames; i++)
            {
                (this as IAnimationClipInternal).Sprites.Add(new Sprite(Sprite.Texture,
                    new Rect(i * frameWidth, Sprite.SourceRect.Y, frameWidth, Sprite.SourceRect.Height)));
            }
        }
    }
}
