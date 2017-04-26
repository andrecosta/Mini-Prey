using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public sealed class Sprite : Entity, ISprite
    {
        public Texture2D Texture { get; }
        public Rect SourceRect { get; private set; }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            Name = texture.Name;

            if (texture.IsLoaded)
                UpdateSourceRect();
            else
                texture.OnLoaded += UpdateSourceRect;
        }

        public Sprite(Texture2D texture, Rect sourceRect) : this(texture)
        {
            SourceRect = sourceRect;
        }

        private void UpdateSourceRect()
        {
            SourceRect = new Rect(0, 0, Texture.Width, Texture.Height);
        }
    }
}
