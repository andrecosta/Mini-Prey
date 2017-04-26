namespace KokoEngine
{
    /// <summary>
    /// Provides sprite rendering capability to a GameObject.
    /// </summary>
    public interface ISpriteRenderer : IComponent
    {
        /// <summary>
        /// The color to apply on the sprite.
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// The sprite asset.
        /// </summary>
        ISprite Sprite { get; set; }

        /// <summary>
        /// The depth layer to render the sprite on.
        /// </summary>
        float Layer { get; set; }
    }
}
