using System.Collections.Generic;

namespace KokoEngine
{
    public sealed class Sprite : Entity, ISprite
    {
        public Texture2D Texture { get; }
        public Rect SourceRect { get; }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            Name = texture.Name;
            SourceRect = new Rect(0, 0, texture.Width, texture.Height);
        }

        public Sprite(Texture2D texture, Rect sourceRect) : this(texture)
        {
            SourceRect = sourceRect;
        }
    }
}
