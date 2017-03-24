using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public sealed class GameObject : Entity, IGameObjectInternal
    {
        public ITransform Transform { get; }
        public string Tag { get; set; }
        public IScene Scene { get; private set; }
        IScene IGameObjectInternal.Scene
        {
            set { Scene = value; }
        }

        private readonly List<IComponent> _components = new List<IComponent>();

        public GameObject()
        {
            // Every GameObject will always have a Transform component by default
            Transform = AddComponent<Transform>();
        }

        public GameObject(string name) : this()
        {
            Name = name;
        }

        /// <summary>
        /// Adds a new component of type T to this GameObject.
        /// </summary>
        /// <typeparam name="T">The type of the component to add</typeparam>
        public T AddComponent<T>() where T : IComponent, new()
        {
            // Instantiate the component
            var c = new T();

            // Set the component's GameObject property to the current GameObject
            var componentInternal = c as IComponentInternal;
            if (componentInternal != null) componentInternal.GameObject = this;

            // Add the instantiated component to the GameObject's component list
            _components.Add(c);

            return c;
        }

        /// <summary>
        /// Returns the first component of type T associated with this GameObject.
        /// If one isn't found, return null.
        /// </summary>
        /// <typeparam name="T">The type of the component to get</typeparam>
        public T GetComponent<T>() where T : IComponent
        {
            return (T) _components.FirstOrDefault(c => c is T);
        }

        /// <summary>
        /// Returns all the components associated with this GameObject.
        /// </summary>
        public List<IComponent> GetComponents() => _components;
    }
}
