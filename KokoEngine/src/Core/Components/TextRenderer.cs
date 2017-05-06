namespace KokoEngine
{
    public class TextRenderer : Component, ITextRenderer
    {
        public Font Font { get; set; }
        public string Text { get; set; }
        public Vector2 Offset { get; set; }
        public Color Color { get; set; } = Color.White;
        public float Size { get; set; } = 1;
    }
}
