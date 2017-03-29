namespace KokoEngine
{
    public interface ISpriteRenderer : IComponent
    {
        Color color { get; set; }
        Sprite sprite { get; set; }
    }
}
