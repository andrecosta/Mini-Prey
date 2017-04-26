namespace KokoEngine
{
    public class SpriteRenderer : Component, ISpriteRenderer
    {
        public ISprite Sprite { get; set; }
        public Color Color { get; set; } = Color.White;
        public float Layer { get; set; }
    }
}
