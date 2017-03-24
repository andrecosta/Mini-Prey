namespace KokoEngine
{
    public struct Color
    {
        public int R;
        public int G;
        public int B;

        public static Color White => new Color(255, 255, 255);
        public static Color Black => new Color(0, 0, 0);
        public static Color Red => new Color(255, 0, 0);
        public static Color Green => new Color(0, 255, 0);
        public static Color Blue => new Color(0, 0, 255);

        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}