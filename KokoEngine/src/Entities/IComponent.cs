// Questions
// - We need that every object inherits from Entity (for Guid and Name)
// - If we want to make (by design) the client inherit from 'Component' to create their own components, instead of creating his own implementation based on IComponent

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KokoEngine
{
    /// <summary>
    /// Interface that defines a component which can be added to a GameObject
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// The GameObject to with this component belongs to
        /// </summary>
        GameObject GameObject { get; }
        Transform Transform { get; }

        T GetComponent<T>() where T : IComponent;
        List<IComponent> GetComponents();
    }

    internal interface IComponentInternal : IComponent
    {
        GameObject GameObject { set; }

        void Awake();
        void Start();
        void Update(float dt);
    }
}
