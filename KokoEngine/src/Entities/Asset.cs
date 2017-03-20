using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KokoEngine
{
    public abstract class Asset : Entity, IAsset
    {
        public object RawData { get; set; }
    }
}
