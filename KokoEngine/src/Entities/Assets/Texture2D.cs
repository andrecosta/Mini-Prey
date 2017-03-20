using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KokoEngine
{
    public class Texture2D : Asset
    {
        public int Width { get; }
        public int Height { get; }

        public Texture2D(string name, object rawData, int width, int height)
        {
            Name = name;
            RawData = rawData;
            Width = width;
            Height = height;
        }
    }
}
