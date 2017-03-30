namespace KokoEngine
{
    public interface ISpriteRenderer : IComponent
    {
        Color color { get; set; }
        ISprite sprite { get; set; }
    }
}
