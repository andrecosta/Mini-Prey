﻿using System.Collections.Generic;

namespace KokoEngine
{
    /// <summary>
    /// Defines a component which can be attached to a GameObject.
    /// </summary>
    public interface IComponent : IEntity
    {
        /// <summary>
        /// The GameObject to which this component is attached.
        /// </summary>
        IGameObject GameObject { get; }

        /// <summary>
        /// The transform of the GameObject to which this component is attached.
        /// </summary>
        ITransform Transform { get; }

        /// <summary>
        /// Proxy method which calls the <see cref="IGameObject.GetComponent{T}"/> of the GameObject to which this component is attached.
        /// </summary>
        /// <typeparam name="T">The type of the component to get.</typeparam>
        /// <returns>The component.</returns>
        T GetComponent<T>() where T : IComponent;

        /// <summary>
        /// Proxy method which calls the <see cref="IGameObject.GetComponents"/> of the GameObject to which this component is attached.
        /// </summary>
        /// <returns>The components attached to the GameObject.</returns>
        List<IComponent> GetComponents();
    }
}