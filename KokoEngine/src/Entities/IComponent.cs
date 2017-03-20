using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KokoEngine
{
    public interface IComponent
    {
        GameObject GameObject { get; }
        Transform Transform { get; }

        T GetComponent<T>() where T : IComponent;
        List<IComponent> GetComponents();
    }
}
