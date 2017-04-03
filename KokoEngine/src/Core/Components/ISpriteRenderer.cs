namespace KokoEngine
{
    /// <summary>
    /// Renders a sprite.
    /// </summary>
    public interface ISpriteRenderer : IComponent
    {
        /// <summary>
        /// The color to apply on the sprite.
        /// </summary>
        Color color { get; set; }

        /// <summary>
        /// The sprite asset.
        /// </summary>
        ISprite sprite { get; set; }
    }
}
