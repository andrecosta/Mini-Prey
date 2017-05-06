namespace KokoEngine
{
    /// <summary>
    /// Provides text rendering capability to a GameObject.
    /// </summary>
    public interface ILineRenderer : IComponent
    {
        Vector2 Start { get; set; }
        Vector2 End { get; set; }
        int Size { get; set; }
        Color Color { get; set; }
    }
}
