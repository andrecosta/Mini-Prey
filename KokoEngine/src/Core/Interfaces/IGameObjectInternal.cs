namespace KokoEngine
{
    internal interface IGameObjectInternal : IGameObject
    {
        IScene Scene { set; }
        IInputManager InputManager { get; set; }
        IScreenManager ScreenManager { get; set; }
    }
}
