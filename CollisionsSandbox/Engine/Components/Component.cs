using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniPrey.Engine.SceneManagement;

namespace MiniPrey.Engine
{
    abstract class Component
    {
        public GameObject GameObject { get; set; } // TODO: Make set private
        public Transform Transform => GameObject.Transform;

        public T GetComponent<T>() where T : Component
        {
            return GameObject.GetComponent<T>();
        }

        public List<Component> GetComponents()
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
