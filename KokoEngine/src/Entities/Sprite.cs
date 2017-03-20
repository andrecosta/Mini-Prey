using System;

namespace KokoEngine
{
    public sealed class Sprite : Entity
    {
        public Texture2D texture;

        public Sprite(string textureName)
        {
            texture = AssetManager.Instance.GetAsset<Texture2D>(textureName);
            Name = textureName;
        }
    }
}
