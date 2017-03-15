using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public class GameObject
    {
        public Transform Transform { get; set; }
        public string Tag { get; set; }

        private List<Component> _components = new List<Component>();

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
        public T AddComponent<T>() where T : Component, new()
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
        public T GetComponent<T>() where T : Component
        {
            return _components.FirstOrDefault(c => c is T) as T;
        }

        /// <summary>
        /// Returns all the components associated with this game object
        /// </summary>
        /// <returns></returns>
        public List<Component> GetComponents()
        {
            return _components;
        }
    }
}
