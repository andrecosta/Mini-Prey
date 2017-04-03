namespace KokoEngine
{
    /// <summary>
    /// Stores a texture object and its usable bounds.
    /// </summary>
    public interface ISprite : IEntity
    {
        /// <summary>
        /// The texture asset.
        /// </summary>
        Texture2D Texture { get; }

        /// <summary>
        /// The source rectangle used to determine the usable bounds of the texture.
        /// This is used, for example, for defining a specific section of a spritesheet.
        /// </summary>
        Rect SourceRect { get; }
    }
}
