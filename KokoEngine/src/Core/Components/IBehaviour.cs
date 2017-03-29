namespace KokoEngine
{
    public interface IBehaviour : IComponent
    {
        bool Enabled { get; set; }
    }
}