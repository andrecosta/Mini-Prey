namespace KokoEngine
{
    public sealed class Sprite : Entity, ISprite
    {
        public Texture2D texture { get; }

        public Sprite(string textureName)
        {
            texture = AssetManager.Instance.GetAsset<Texture2D>(textureName);
            Name = textureName;
        }
    }
}
