using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KokoEngine
{
    public struct Color
    {
        public int R;
        public int G;
        public int B;

        public static Color Red => new Color(255, 0, 0);
        public static Color White => new Color(255, 255, 255);

        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}