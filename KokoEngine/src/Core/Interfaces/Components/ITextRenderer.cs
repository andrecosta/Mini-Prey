namespace KokoEngine
{
    /// <summary>
    /// Provides text rendering capability to a GameObject.
    /// </summary>
    public interface ITextRenderer : IComponent
    {
        Font Font { get; set; }

        /// <summary>
        /// The text to be displayed.
        /// </summary>
        string Text { get; set; }

        Vector2 Offset { get; set; }

        /// <summary>
        /// The color to apply on the text.
        /// </summary>
        Color Color { get; set; }

        float Size { get; set; }
    }
}
