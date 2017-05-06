using System.Collections.Generic;

namespace KokoEngine
{
    public abstract class Component : Entity, IComponentInternal
    {
        public override string Name => GameObject.Name;
        public IGameObject GameObject { get; private set; }
        IGameObject IComponentInternal.GameObject { set { GameObject = value; } }
        public ITransform Transform => GameObject.Transform;
        protected IInputManager Input => Engine.Instance.InputManager;
        protected IScreenManager Screen => Engine.Instance.ScreenManager;
        protected ITimeManager Time => Engine.Instance.TimeManager;

        internal Component() { }

        // Proxy methods for convenience
        public T AddComponent<T>() where T : IComponent, new() => GameObject.AddComponent<T>();
        public T GetComponent<T>() where T : IComponent => GameObject.GetComponent<T>();
        public List<IComponent> GetComponents() => GameObject.GetComponents();

        // Update
        protected virtual void Update() { }
        void IComponentInternal.Update() => Update();
    }
}
