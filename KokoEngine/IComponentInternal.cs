using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KokoEngine
{
    internal interface IComponentInternal : IComponent
    {
        void SetGameObject(GameObject go);
    }
}
