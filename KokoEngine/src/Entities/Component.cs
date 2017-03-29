﻿using System.Collections.Generic;

namespace KokoEngine
{
    public abstract class Component : Entity, IComponentInternal
    {
        public override string Name => GameObject.Name;
        public IGameObject GameObject { get; private set; }
        IGameObject IComponentInternal.GameObject { set { GameObject = value; } }
        public ITransform Transform => GameObject.Transform;

        internal Component() { }

        // Component management
        public T GetComponent<T>() where T : IComponent => GameObject.GetComponent<T>();
        public List<IComponent> GetComponents() => GameObject.GetComponents();


        protected virtual void Awake() { }
        void IComponentInternal.Awake() => Awake();

        protected virtual void Start() { }
        void IComponentInternal.Start() => Start();

        // Update
        protected virtual void Update(float dt) { }
        void IComponentInternal.Update(float dt) => Update(dt);

        protected virtual void End() { }
        void IComponentInternal.End() => End();
    }
}
