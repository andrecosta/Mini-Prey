namespace KokoEngine
{
    public class SpriteRenderer : Component, ISpriteRenderer
    {
        public ISprite sprite { get; set; }
        public Color color { get; set; }

        public SpriteRenderer()
        {
            color = Color.White;
        }
    }
}
