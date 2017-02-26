using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPrey.Engine
{
    abstract class Component
    {
        public GameObject gameObject { get; set;  }
        public Transform transform => gameObject.transform;
        
        public T GetComponent<T>() where T : Component
        {
            return gameObject.GetComponent<T>();
        }

        public List<Component> GetComponents()
        {
            return gameObject.GetComponents();
        }

        public virtual void Awake()
        {
        }

        public virtual void Start()
        {
        }

        public virtual void Update(float dt)
        {
        }
    }
}
