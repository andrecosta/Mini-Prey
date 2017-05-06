namespace KokoEngine
{
    public class LineRenderer : Component, ILineRenderer
    {
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }
        public int Size { get; set; }
        public Color Color { get; set; } = Color.White;
    }
}
