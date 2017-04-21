using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public class Engine
    {
        // Managers
        public IAssetManager AssetManager { get; }
        public IInputManager InputManager { get; }
        public IScreenManager ScreenManager { get; }
        public ISceneManager SceneManager { get; }
        public ITimeManager TimeManager { get; }

        // Time tracking and control
        internal float DeltaTime { get; private set; }
        internal double TotalTime { get; private set; }
        internal float TimeScale { get; set; } = 1;

        public Engine(IAssetManager assetManager, IInputManager inputManager, IScreenManager screenManager, ISceneManager sceneManager)
        {
            // Managers
            AssetManager = assetManager;
            InputManager = inputManager;
            ScreenManager = screenManager;
            SceneManager = sceneManager;

            // Default options

            // Setup the static helper classes
            //Screen.ManagerInstance = screenManager;
            //Time.ManagerInstance = timeManager;
            //Input.ManagerInstance = inputManager;
        }

        public void Initialize()
        {
            // Load the first scene
            SceneManager.LoadScene(0);
        }

        public void Update(float dt)
        {
            dt *= Time.TimeScale;
            DeltaTime = dt;
            TotalTime += dt;

            (InputManager as IInputManagerInternal)?.Update(dt);
            (SceneManager as ISceneManagerInternal)?.UpdateActiveScene(dt);

            // Draw the active scene's game objects which contain renderable components
            //foreach (var rootGameObject in _sceneManager.GetActiveScene().GetRootGameObjects())
            //    PlaySounds(rootGameObject);
        }
    }
}
