using System;
using KokoEngine;

namespace MiniPreyGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static partial class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /* -------------------------------------------------------------------------------------------------
             * INITIAL ENGINE CONFIG
             * -------------------------------------------------------------------------------------------------
             * Here we will instantiate the concrete implementations that the engine needs in order to function.
             * This will also be where we will configure them and hook everything up.
             * -------------------------------------------------------------------------------------------------
             * Implementations of the following interfaces are needed:
             * - IAssetManager
             * - IInputManager
             * - IScreenManager
             * - ISceneManager
             * 
             * TODO: THESE CONFIGURATIONS COULD COME FROM A FILE OR AN EDITOR IN THE ENGINE STARTUP STAGE!
             */

            // Instantiate the managers
            IAssetManager assetManager = new AssetManager();
            IInputManager inputManager = new InputManager();
            IScreenManager screenManager = new ScreenManager();
            ISceneManager sceneManager = new SceneManager();

            // Setup the asset manager
            assetManager.LoadAsset<Texture2D>("boid.png");
            assetManager.LoadAsset<Texture2D>("boid_rainbow.png");
            assetManager.LoadAsset<Texture2D>("waypoint_red.png");
            assetManager.LoadAsset<Texture2D>("player.png");
            assetManager.LoadAsset<AudioClip>("seekSound.wav");
            assetManager.LoadAsset<AudioClip>("fleeSound.wav");
            assetManager.LoadAsset<Font>("debug.spritefont");

            // Setup the input manager
            inputManager.AddActionBinding("Jump", "Space");
            inputManager.AddActionBinding("Fire", "F", "X", "C", "V");
            inputManager.AddAxisBinding("Horizontal", "A", "D", 10);
            inputManager.AddAxisBinding("Vertical", "W", "S", 10);
            inputManager.AddAxisBinding("Horizontal", "Left", "Right", 10);
            inputManager.AddAxisBinding("Vertical", "Up", "Down", 10);

            // Setup the screen manager
            screenManager.AddSupportedResolution(1280, 720);
            screenManager.IsFullscreen = false;

            // Setup the scenes that will be usable in the game
            // Load .scene files
            //SetupScene(assetManager, sceneManager);

            // Instantiate the engine and set it up
            Engine engine = new Engine(assetManager, inputManager, screenManager, sceneManager);

            // Start MonoGame
            // MonoGame will load the resources and bind call the engine's update method
            using (var game = new Game1(engine))
                game.Run();
        }
    }
#endif
}
