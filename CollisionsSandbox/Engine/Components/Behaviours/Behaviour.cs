using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPrey.Engine
{
    abstract class Behaviour : Component
    {
        public bool enabled { get; set; }
    }
}
