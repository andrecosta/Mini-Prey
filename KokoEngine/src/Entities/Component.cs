using System;
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
        
        public T GetComponent<T>() where T : IComponent
        {
            return GameObject.GetComponent<T>();
        }

        public List<IComponent> GetComponents()
        {
            return GameObject.GetComponents();
        }

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update(float dt) { }
    }
}
