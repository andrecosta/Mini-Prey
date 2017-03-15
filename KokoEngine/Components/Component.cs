using System.Collections.Generic;

namespace KokoEngine
{
    public abstract class Component : IComponent
    {
        public GameObject GameObject { get; set; } // TODO: Make set private
        public Transform Transform => GameObject.Transform;

        public T GetComponent<T>() where T : IComponent
        {
            return GameObject.GetComponent<T>();
        }

        public List<IComponent> GetComponents()
        {
            return GameObject.GetComponents();
        }

        // TODO: This should not be here
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update(float dt) { }
        public virtual void End() { }
    }
}
