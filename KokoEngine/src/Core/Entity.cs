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

        public override string ToString()
        {
            return $"{Name} [GUID={Guid}]";
        }

        public static bool operator true(Entity e)
        {
            return e != null;
        }

        public static bool operator false(Entity e)
        {
            return e == null;
        }
    }
}
