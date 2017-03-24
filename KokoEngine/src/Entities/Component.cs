using System.Collections.Generic;

namespace KokoEngine
{
    public abstract class Component : Entity, IComponentInternal
    {
        public override string Name => GameObject.Name;
        public IGameObject GameObject { get; private set; }
        IGameObject IComponentInternal.GameObject
        {
            set { GameObject = value; }
        }
        public ITransform Transform => GameObject.Transform;

        public Component(IGameObject go)
        {
            GameObject = go;
        }

        public T GetComponent<T>() where T : IComponent => GameObject.GetComponent<T>();
        public List<IComponent> GetComponents() => GameObject.GetComponents();

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update(float dt) { }
    }
}
