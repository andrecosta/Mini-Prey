using System;
using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class GameObject
    {
        public Transform Transform { get; set; }
        public string Tag { get; set; }

        private List<IComponent> _components = new List<IComponent>();

        public GameObject()
        {
            SceneManager.GetActiveScene().AddRootGameObject(this);

            // Every game object will always have a Transform component by default
            Transform = AddComponent<Transform>();
        }

        /// <summary>
        /// Adds a new component of type T to this game object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddComponent<T>() where T : IComponent, new()
        {
            T c = new T();
            c.GameObject = this;
            _components.Add(c);
            return c;
        }

        /// <summary>
        /// Returns the first component of type T associated with this game object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : IComponent
        {
            return (T)_components.FirstOrDefault(c => c is T);
        }

        /// <summary>
        /// Returns all the components associated with this game object
        /// </summary>
        /// <returns></returns>
        public List<IComponent> GetComponents()
        {
            return _components;
        }
    }
}
