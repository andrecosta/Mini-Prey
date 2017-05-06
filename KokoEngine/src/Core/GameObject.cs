using System.Collections.Generic;
using System.Linq;

namespace KokoEngine
{
    public sealed class GameObject : Entity, IGameObjectInternal
    {
        public ITransform Transform { get; }
        public string Tag { get; set; }
        public bool IsActive { get; private set; } = true;
        ISceneInternal IGameObjectInternal.Scene { get; set; }

        private readonly List<IComponent> _components = new List<IComponent>();

        internal GameObject(string name, ISceneInternal scene)
        {
            Name = name;
            (this as IGameObjectInternal).Scene = scene;

            // Every GameObject will always have a Transform component by default
            Transform = AddComponent<Transform>();
        }
        
        public T AddComponent<T>() where T : IComponent, new()
        {
            // Instantiate the component
            T component = new T();

            // Set the component's GameObject property to the current GameObject
            IComponentInternal componentInternal = component as IComponentInternal;
            if (componentInternal != null)
                componentInternal.GameObject = this;

            // Add the instantiated component to the GameObject's component list
            _components.Add(component);

            return component;
        }
        
        public T GetComponent<T>() where T : IComponent
        {
            return (T) _components.FirstOrDefault(c => c is T);
        }
        
        public List<IComponent> GetComponents() => _components;

        public void SetActive(bool active)
        {
            IsActive = active;
        }
    }
}
