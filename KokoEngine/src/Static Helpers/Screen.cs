namespace KokoEngine
{
    public static class Screen
    {
        public static int Width { get; internal set; }
        public static int Height { get; internal set; }

        public static void SetResolution(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
