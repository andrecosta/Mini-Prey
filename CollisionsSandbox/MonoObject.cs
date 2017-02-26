using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniPrey
{
    abstract class MonoObject
    {
        private Texture2D _texture;
        public Vector2 Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        protected MonoObject(Texture2D texture)
        {
            _texture = texture;
            Color = Color.White;
        }

        public virtual void Update(float dt)
        {
            
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color);
        }
    }
}
