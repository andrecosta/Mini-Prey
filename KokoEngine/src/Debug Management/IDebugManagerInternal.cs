namespace KokoEngine
{
    internal interface IDebugManagerInternal : IDebugManager
    {
        void Initialize();
        void Update();
        void Render();
    }
}
