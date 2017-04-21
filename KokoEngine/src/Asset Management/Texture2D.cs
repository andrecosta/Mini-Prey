namespace KokoEngine
{
    public sealed class Texture2D : Asset
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public void SetData(object rawData, int width, int height)
        {
            Width = width;
            Height = height;

            base.SetData(rawData);
        }
    }
}
