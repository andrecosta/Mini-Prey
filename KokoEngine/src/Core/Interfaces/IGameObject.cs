using System.Collections.Generic;

namespace KokoEngine
{
    /// <summary>
    /// Base class for all entities used in a Scene. A GameObject is essentially a container for <see cref="IComponent"/>s.
    /// </summary>
    public interface IGameObject : IEntity
    {
        /// <summary>
        /// The transform component attached to this GameObject. Every GameObject has this component by default.
        /// </summary>
        ITransform Transform { get; }

        /// <summary>
        /// The tag of this GameObject.
        /// </summary>
        string Tag { get; set; }

        /// <summary>
        /// Whether the GameObject is active or not.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// The Scene this GameObject belongs to.
        /// </summary>
        IScene Scene { get; }

        /// <summary>
        /// Adds a new component of type T to this GameObject.
        /// </summary>
        /// <typeparam name="T">The type of the component to add.</typeparam>
        /// <returns>The created component.</returns>
        T AddComponent<T>() where T : IComponent, new();

        /// <summary>
        /// Returns the first component of type T attached to this GameObject.
        /// If one isn't found, returns null.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component.</returns>
        T GetComponent<T>() where T : IComponent;

        /// <summary>
        /// Returns all the components attached to this GameObject.
        /// </summary>
        List<IComponent> GetComponents();

        /// <summary>
        /// Sets the GameObject as either active or not active. Inactive objects will be hidden.
        /// </summary>
        /// <param name="active">The state to set.</param>
        void SetActive(bool active);
    }
}