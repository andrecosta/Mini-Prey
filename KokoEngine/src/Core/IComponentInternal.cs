namespace KokoEngine
{
    internal interface IComponentInternal : IComponent
    {
        IGameObject GameObject { set; }

        void Awake();
        void Start();
        void Update(float dt);
    }
}
