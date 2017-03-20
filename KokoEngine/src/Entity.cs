using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KokoEngine
{
    public abstract class Entity : IEntity
    {
        public string Guid { get; private set; }
        public virtual string Name { get; protected set; }

        protected Entity()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"KokoEngine Entity (NAME={Name}, GUID={Guid})";
        }
    }
}
