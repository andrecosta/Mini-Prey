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
             * - ITimeManager
             * - ISceneManager
             * 
             * TODO: IN THE FUTURE, THESE CONFIGURATIONS COULD COME FROM A FILE OR AN EDITOR IN THE ENGINE STARTUP STAGE!
             */

            // Instantiate the managers
            IAssetManager assetManager = new AssetManager();
            IInputManager inputManager = new InputManager();
            IScreenManager screenManager = new ScreenManager();
            ITimeManager timeManager = new TimeManager();
            ISceneManager sceneManager = new SceneManager();
            IRenderManager renderManager = new RenderManager();

            // Setup the asset manager
            assetManager.RootDirectory = "Content";
            assetManager.AddAsset<Texture2D>("boid.png");
            assetManager.AddAsset<Texture2D>("boid_rainbow.png");
            assetManager.AddAsset<Texture2D>("waypoint_red.png");
            assetManager.AddAsset<Texture2D>("player.png");
            assetManager.AddAsset<AudioClip>("seekSound.wav");
            assetManager.AddAsset<AudioClip>("fleeSound.wav");
            assetManager.AddAsset<Font>("debug.spritefont");

            // Setup the input manager
            inputManager.AddActionBinding("Jump", "Space");
            inputManager.AddActionBinding("Fire", "F", "X", "C", "V");
            inputManager.AddAxisBinding("Horizontal", "A", "D", 10);
            inputManager.AddAxisBinding("Vertical", "W", "S", 10);
            inputManager.AddAxisBinding("Horizontal", "Left", "Right", 10);
            inputManager.AddAxisBinding("Vertical", "Up", "Down", 10);

            // Setup the screen manager
            screenManager.AddSupportedResolution(1280, 720);
            // TODO: set resolution!
            screenManager.IsFullscreen = false;

            // Setup the time manager
            timeManager.TimeScale = 1;

            // Setup the scenes that will be usable in the game
            //IScene myScene = SetupScenePrototype(assetManager);
            IScene myScene = SetupScene(assetManager);
            sceneManager.AddScene(myScene);

            // Instantiate the engine with the settings created above
            IEngine engine = new Engine(assetManager, inputManager, screenManager, timeManager, sceneManager, renderManager);

            // Start MonoGame
            // MonoGame will load the resources and call the engine's update method
            using (var game = new Game1(engine))
                game.Run();
        }
    }
#endif
}
