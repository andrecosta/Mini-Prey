using System;
using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class GameObject : Entity
    {
        public Transform Transform { get; private set; }
        public string Tag { get; set; }

        private readonly List<IComponent> _components = new List<IComponent>();

        public GameObject()
        {
            SceneManager.GetActiveScene().AddRootGameObject(this);

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
            T c = new T();
            ((IComponentInternal)c).SetGameObject(this);
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
            return (T)_components.FirstOrDefault(c => c is T);
        }

        /// <summary>
        /// Returns all the components associated with this GameObject.
        /// </summary>
        public List<IComponent> GetComponents()
        {
            return _components;
        }
    }
}
