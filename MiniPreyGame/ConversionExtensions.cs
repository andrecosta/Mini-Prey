using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniPreyGame
{
    /// <summary>
    /// Extensions to convert between formats common to MonoGame and KokoEngine
    /// </summary>
    public static class ConversionExtensions
    {
        #region Mono to Koko
        public static KokoEngine.Vector2 ToKokoVector2(this Vector2 input)
        {
            return new KokoEngine.Vector2(input.X, input.Y);
        }

        public static KokoEngine.Vector2 ToKokoVector2(this Point input)
        {
            return new KokoEngine.Vector2(input.X, input.Y);
        }

        public static KokoEngine.Color ToKokoColor(this Color input)
        {
            return new KokoEngine.Color(input.R, input.G, input.B);
        }
        #endregion

        #region Koko to Mono
        public static Vector2 ToMonoVector2(this KokoEngine.Vector2 input)
        {
            return new Vector2(input.X, input.Y);
        }

        public static Point ToMonoPoint(this KokoEngine.Vector2 input)
        {
            return new Point((int) input.X, (int) input.Y);
        }

        public static Color ToMonoColor(this KokoEngine.Color input)
        {
            return new Color(input.R, input.G, input.B);
        }

        public static Texture2D ToMonoTexture2D(this KokoEngine.Texture2D input)
        {
            return input.RawData as Texture2D;
        }

        public static Rectangle ToMonoRectangle(this KokoEngine.Rect input)
        {
            return new Rectangle(input.X, input.Y, input.Width, input.Height);
        }

        #endregion
    }
}
