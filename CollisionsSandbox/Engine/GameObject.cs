using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniPrey.Engine.SceneManagement;

namespace MiniPrey.Engine
{
    class GameObject
    {
        public Transform transform { get; set; }
        public string tag { get; set; }

        List<Component> _components = new List<Component>();

        public GameObject()
        {
            SceneManager.GetActiveScene().AddRootGameObject(this);

            // Every game object will always have a Transform component by default
            transform = AddComponent<Transform>();
        }

        /// <summary>
        /// Adds a new component of type T to this game object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddComponent<T>() where T : Component, new()
        {
            T component = new T();
            component.gameObject = this;
            if (transform != null)
                component.Awake();
            _components.Add(component);
            return component;
        }

        /// <summary>
        /// Returns the first component of type T associated with this game object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if (component is T)
                    return (T)component;
            }

            return default(T);
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
