/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace KokoEngine
{
    interface IRenderSystem
    {
        int GetWindowWidth();
        int GetWindowHeight();
        void Draw(Texture2D texture, Vector2 position);
    }

    class MonoGameRenderSystem : IRenderSystem
    {
        private SpriteBatch _spriteBatch;
        private GraphicsDevice _graphicsDevice;

        public MonoGameRenderSystem(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public int GetWindowWidth()
        {
            return 0;
        }

        public int GetWindowHeight()
        {
            return 0;
        }

        public void Draw(Texture2D texture, Vector2 position)
        {
            Microsoft.Xna.Framework.Graphics.Texture2D tex = new Microsoft.Xna.Framework.Graphics.Texture2D();
            _spriteBatch.Draw();
        }
    }
}
*/