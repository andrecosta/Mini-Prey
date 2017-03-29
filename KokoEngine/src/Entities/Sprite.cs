namespace KokoEngine
{
    public sealed class Sprite : Entity, ISprite
    {
        public Texture2D texture { get; }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            Name = texture.Name;
        }
    }
}
