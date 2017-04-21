using System;
using System.Collections.Generic;

namespace KokoEngine
{
    public class Engine
    {
        public ISceneManager SceneManager { get; }
        public IAssetManager AssetManager { get; }
        public IInputManager InputManager { get; }

        public Engine(IAssetManager assetManager, IInputManager inputManager, ISceneManager sceneManager)
        {
            // Managers
            SceneManager = sceneManager;
            AssetManager = assetManager;
            InputManager = inputManager;

            // Static helper classes
            Screen.SetResolution(1280, 720);
            Time.TimeScale = 1;
            Input.Init(InputManager); // TODO: CLEANUP
        }

        public void Initialize()
        {
            SceneManager.LoadScene(0);
        }

        public void Update(float dt)
        {
            dt *= Time.TimeScale;
            Time.DeltaTime = dt;
            Time.TotalTime += dt;

            InputManager.Update(dt);
            SceneManager.UpdateActiveScene(dt);

            // Draw the active scene's game objects which contain renderable components
            //foreach (var rootGameObject in _sceneManager.GetActiveScene().GetRootGameObjects())
            //    PlaySounds(rootGameObject);
        }
    }
}
