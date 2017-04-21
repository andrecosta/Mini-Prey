using Microsoft.Xna.Framework;

namespace MiniPreyGame
{
    /// <summary>
    /// Conversion extensions between formats common between MonoGame and KokoEngine
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
        #endregion
    }
}
