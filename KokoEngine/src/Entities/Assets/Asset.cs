namespace KokoEngine
{
    public abstract class Asset : Entity, IAsset
    {
        public object RawData { get; set; }
    }
}
