namespace KokoEngine
{
    public abstract class Entity : IEntity
    {
        public string Guid { get; }
        public virtual string Name { get; protected set; } = "KokoEngine Entity";

        internal Entity()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        public override string ToString() => $"{Name} [GUID={Guid}]";

        public static bool operator true(Entity e) => e != null;
        public static bool operator false(Entity e) => e == null;
        public static bool operator !(Entity e) => e == null;
    }
}
