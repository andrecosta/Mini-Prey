using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public abstract class Component : Entity, IComponentInternal
    {
        public override string Name => GameObject.Name;
        public GameObject GameObject { get; private set; }
        GameObject IComponentInternal.GameObject
        {
            set { GameObject = value; }
        }

        public Transform Transform => GameObject.Transform;
        
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
