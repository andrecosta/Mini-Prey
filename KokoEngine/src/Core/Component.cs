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
        protected IDebugManager Debug => Engine.Instance.DebugManager;

        internal Component() { }

        // Proxy methods for convenient access from user scripts
        public T AddComponent<T>() where T : IComponent, new() => GameObject.AddComponent<T>();
        public T GetComponent<T>() where T : IComponent => GameObject.GetComponent<T>();
        public List<T> GetComponents<T>() where T : IComponent => GameObject.GetComponents<T>();
        public List<T> FindObjectsOfType<T>() where T : IComponent => (GameObject as IGameObjectInternal).Scene.FindObjectsOfType<T>();
        public T FindObjectOfType<T>() where T : IComponent => (GameObject as IGameObjectInternal).Scene.FindObjectOfType<T>();

        // Update
        protected virtual void Update() { }
        void IComponentInternal.Update() => Update();
    }
}
