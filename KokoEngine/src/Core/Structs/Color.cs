﻿namespace KokoEngine
{
    public struct Color
    {
        public int R;
        public int G;
        public int B;
        public int A;

        public static Color White => new Color(255, 255, 255);
        public static Color Black => new Color(0, 0, 0);
        public static Color Grey => new Color(128, 128, 128);
        public static Color Red => new Color(255, 0, 0);
        public static Color Green => new Color(0, 255, 0);
        public static Color Blue => new Color(0, 0, 255);
        public static Color Cyan => new Color(0, 255, 255);
        public static Color BlackTransparent => new Color(0, 0, 0, 128);

        public Color(int r, int g, int b, int a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}