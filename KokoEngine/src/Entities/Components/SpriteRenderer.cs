namespace KokoEngine
{
    public class SpriteRenderer : Component, ISpriteRenderer
    {
        public Sprite sprite { get; set; }
        public Color color { get; set; }

        public SpriteRenderer()
        {
            color = Color.White;
        }
    }
}
