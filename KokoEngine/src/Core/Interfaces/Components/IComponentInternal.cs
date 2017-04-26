namespace KokoEngine
{
    internal interface IComponentInternal : IComponent
    {
        IGameObject GameObject { set; }

        void Update(float dt);
    }
}
