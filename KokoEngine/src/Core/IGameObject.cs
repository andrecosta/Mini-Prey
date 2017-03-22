using System.Collections.Generic;

namespace KokoEngine
{
    public interface IGameObject : IEntity
    {
        ITransform Transform { get; }
        string Tag { get; set; }
        string Guid { get; }
        string Name { get; }

        /// <summary>
        /// Adds a new component of type T to this GameObject.
        /// </summary>
        /// <typeparam name="T">The type of the component to add</typeparam>
        T AddComponent<T>() where T : IComponent, new();

        /// <summary>
        /// Returns the first component of type T associated with this GameObject.
        /// If one isn't found, return null.
        /// </summary>
        /// <typeparam name="T">The type of the component to get</typeparam>
        T GetComponent<T>() where T : IComponent;

        /// <summary>
        /// Returns all the components associated with this GameObject.
        /// </summary>
        List<IComponent> GetComponents();
    }
}