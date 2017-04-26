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

        IInputManager IGameObjectInternal.InputManager { get; set; }
        IScreenManager IGameObjectInternal.ScreenManager { get; set; }

        private readonly List<IComponent> _components = new List<IComponent>();

        public GameObject()
        {
            // Every GameObject will always have a Transform component by default
            Transform = AddComponent<Transform>();
        }

        public GameObject(string name, IInputManager inputManager, IScreenManager screenManager) : this()
        {
            Name = name;
            (this as IGameObjectInternal).InputManager = inputManager;
            (this as IGameObjectInternal).ScreenManager = screenManager;
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
    }
}
