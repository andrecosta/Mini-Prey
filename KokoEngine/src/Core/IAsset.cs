using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KokoEngine
{
    public interface IAsset : IEntity
    {
        object RawData { get; set; }
    }
}
