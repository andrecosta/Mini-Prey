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
             * This will also be where we will hook it all up.
             * -------------------------------------------------------------------------------------------------
             * Implementations of the following interfaces are needed:
             * - IAssetManager
             * - IInputManager
             * - ISceneManager
             * 
             * NOTE: THESE CONFIGURATIONS COULD COME FROM A FILE OR AN EDITOR!
             */

            // Instantiate the managers
            IAssetManager assetManager = new AssetManager();
            IInputManager inputManager = new InputManager();
            ISceneManager sceneManager = new SceneManager();

            // Setup assets to load
            assetManager.LoadAsset<Texture2D>("boid.png");
            assetManager.LoadAsset<Texture2D>("boid_rainbow.png");
            assetManager.LoadAsset<Texture2D>("waypoint_red.png");
            assetManager.LoadAsset<Texture2D>("player.png");
            assetManager.LoadAsset<AudioClip>("seekSound.wav");
            assetManager.LoadAsset<AudioClip>("fleeSound.wav");
            assetManager.LoadAsset<Font>("debug.spritefont");

            // Setup input bindings
            inputManager.AddActionBinding("Jump", "Space");
            inputManager.AddActionBinding("Fire", "F", "X", "C", "V");
            inputManager.AddAxisBinding("Horizontal", "A", "D", 10);
            inputManager.AddAxisBinding("Vertical", "W", "S", 10);
            inputManager.AddAxisBinding("Horizontal", "Left", "Right", 10);
            inputManager.AddAxisBinding("Vertical", "Up", "Down", 10);

            // Setup the scenes that will be usable in the game
            // Load .scene files
            //SetupScene(assetManager, sceneManager);

            // Instantiate the engine and set it up
            Engine engine = new Engine(assetManager, inputManager, sceneManager);

            // Start MonoGame
            // MonoGame will load the resources and bind call the engine's update method
            using (var game = new Game1(engine))
                game.Run();
        }
    }
#endif
}
