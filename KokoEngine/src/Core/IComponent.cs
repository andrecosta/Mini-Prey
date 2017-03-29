using System.Collections.Generic;

namespace KokoEngine
{
    /// <summary>
    /// Interface that defines a component which can be added to a GameObject
    /// </summary>
    public interface IComponent : IEntity
    {
        /// <summary>
        /// The GameObject to with this component belongs to
        /// </summary>
        IGameObject GameObject { get; }
        ITransform Transform { get; }

        T GetComponent<T>() where T : IComponent;
        List<IComponent> GetComponents();
    }
}
