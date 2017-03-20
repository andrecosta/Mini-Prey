using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KokoEngine
{
    public interface IEntity
    {
        string Guid { get; }
        string Name { get; }
    }
}
