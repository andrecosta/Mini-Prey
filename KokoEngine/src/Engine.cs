using System;

namespace KokoEngine
{
    public class Engine : IEngine
    {
        // Manager references
        private readonly IAssetManager _assetManager;
        private readonly IInputManager _inputManager;
        private readonly IScreenManager _screenManager;
        private readonly ITimeManager _timeManager;
        private readonly ISceneManager _sceneManager;
        private readonly IRenderManager _renderManager;

        // TODO: Create in internal interface!
        internal IInputManager InputManager => _inputManager;
        internal IScreenManager ScreenManager => _screenManager;
        internal ITimeManager TimeManager => _timeManager;
        internal IRenderManager RenderManager => _renderManager;

        internal static Engine Instance { get; private set; }

        // Constructor
        public Engine(IAssetManager assetManager, IInputManager inputManager, IScreenManager screenManager,
            ITimeManager timeManager, ISceneManager sceneManager, IRenderManager renderManager)
        {
            // Managers
            _assetManager = assetManager;
            _inputManager = inputManager;
            _screenManager = screenManager;
            _timeManager = timeManager;
            _sceneManager = sceneManager;
            _renderManager = renderManager;

            Instance = this;
        }

        // Callbacks
        public void Setup(Action<IScreenManager, IAssetManager, IInputManager, IRenderManager> callback)
        {
            _screenManager.SetResolution(1280, 720);
            callback?.Invoke(_screenManager, _assetManager, _inputManager, _renderManager);
        }

        public void Initialize()
        {
            // Initialize subsystems
            (_assetManager as IAssetManagerInternal)?.Initialize();
            (_sceneManager as ISceneManagerInternal)?.Initialize();
        }

        public void Update(float dt)
        {
            // Update all the dynamic subsystems
            // TODO: IManager internal?
            (_inputManager as IInputManagerInternal)?.Update(dt);
            (_timeManager as ITimeManagerInternal)?.Update(dt);
            (_sceneManager as ISceneManagerInternal)?.Update(); // TODO: ?? weird

            // Draw the active scene's game objects which contain renderable components
            //foreach (var rootGameObject in _sceneManager.GetActiveScene().GetRootGameObjects())
            //    PlaySounds(rootGameObject);
        }

        public void Render()
        {
            (_renderManager as IRenderManagerInternal)?.RenderScene(_sceneManager.GetActiveScene());

            //callback?.Invoke(_sceneManager);
        }
    }
}
