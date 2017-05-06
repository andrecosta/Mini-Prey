namespace KokoEngine
{
    internal interface IGameObjectInternal : IGameObject
    {
        ISceneInternal Scene { get; set; }
    }
}
