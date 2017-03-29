using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KokoEngine
{
    public interface ITree<T>
    {
        T Parent { get; set; }
        List<T> Children { get; }
        int ChildCount { get; }
        //void SetParent(ITransform parent, bool keepWorldPosition = true);
        T GetChild(int index);
    }
}
