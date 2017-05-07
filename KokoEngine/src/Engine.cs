using System;

namespace KokoEngine
{
    public class Engine : IEngine
    {
        // Manager references
        private readonly IAssetManagerInternal _assetManager;
        private readonly IInputManagerInternal _inputManager;
        private readonly IScreenManagerInternal _screenManager;
        private readonly ITimeManagerInternal _timeManager;
        private readonly ISceneManagerInternal _sceneManager;
        private readonly IRenderManagerInternal _renderManager;
        private readonly IDebugManagerInternal _debugManager;

        // TODO: Create in internal interface!
        internal IAssetManagerInternal AssetManager => _assetManager;
        internal IInputManagerInternal InputManager => _inputManager;
        internal IScreenManagerInternal ScreenManager => _screenManager;
        internal ITimeManagerInternal TimeManager => _timeManager;
        internal IRenderManagerInternal RenderManager => _renderManager;
        internal IDebugManagerInternal DebugManager => _debugManager;

        internal static Engine Instance { get; private set; }

        // Constructor
        public Engine(IAssetManager assetManager, IInputManager inputManager, IScreenManager screenManager,
            ITimeManager timeManager, ISceneManager sceneManager, IRenderManager renderManager, IDebugManager debugManager)
        {
            // Managers
            _assetManager = assetManager as IAssetManagerInternal;
            _inputManager = inputManager as IInputManagerInternal;
            _screenManager = screenManager as IScreenManagerInternal;
            _timeManager = timeManager as ITimeManagerInternal;
            _sceneManager = sceneManager as ISceneManagerInternal;
            _renderManager = renderManager as IRenderManagerInternal;
            _debugManager = debugManager as IDebugManagerInternal;

            Instance = this;
        }

        // Callbacks
        public void Setup(Action<IScreenManager, IAssetManager, IInputManager, IRenderManager> callback)
        {
            callback?.Invoke(_screenManager, _assetManager, _inputManager, _renderManager);
        }

        public void Initialize()
        {
            // Initialize subsystems
            _assetManager.Initialize();
            _sceneManager.Initialize();
            _debugManager.Initialize();
        }

        public void Update(float dt)
        {
            // Update all the dynamic subsystems
            _inputManager.Update(dt);
            _timeManager.Update(dt);
            _sceneManager.Update();
            _debugManager.Update();
        }

        public void Render()
        {
            _renderManager.RenderScene(_sceneManager.GetActiveScene() as ISceneInternal);
            _debugManager.Render();
        }
    }
}
