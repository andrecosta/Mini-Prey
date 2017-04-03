namespace KokoEngine
{
    /// <summary>
    /// Base class for all objects used by the engine.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        string Guid { get; }

        /// <summary>
        /// The name of the entity.
        /// </summary>
        string Name { get; }
    }
}
