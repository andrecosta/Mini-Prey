namespace KokoEngine
{
    public abstract class Entity : IEntity
    {
        public string Guid { get; }
        public virtual string Name { get; protected set; } = "KokoEngine Entity";

        protected Entity()
        {
            Guid = System.Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{Name} [GUID={Guid}]";
        }
    }
}
