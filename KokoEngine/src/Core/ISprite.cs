namespace KokoEngine
{
    public interface ISprite : IEntity
    {
        Texture2D Texture { get; }
        Rect SourceRect { get; }
    }
}
