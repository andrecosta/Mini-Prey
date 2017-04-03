namespace KokoEngine
{
    /// <summary>
    /// Base class for all engine assets. Wraps a resource's raw data.
    /// </summary>
    public interface IAsset : IEntity
    {
        /// <summary>
        /// The raw data of the asset. This can be cast to any native format used by the client application using the engine.
        /// </summary>
        object RawData { get; set; }
    }
}
