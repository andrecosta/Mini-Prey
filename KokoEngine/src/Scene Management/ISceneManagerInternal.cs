namespace KokoEngine
{
    internal interface ISceneManagerInternal : ISceneManager
    {
        void Initialize();
        void Update(float dt);
    }
}