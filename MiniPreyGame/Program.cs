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
             * - IRenderManager
             * - IDebugManager
             * 
             * TODO: IN THE FUTURE, THESE CONFIGURATIONS COULD COME FROM A FILE OR AN EDITOR !
             */

            // Instantiate the managers
            IAssetManager assetManager = new AssetManager();
            IInputManager inputManager = new InputManager();
            IScreenManager screenManager = new ScreenManager();
            ITimeManager timeManager = new TimeManager();
            ISceneManager sceneManager = new SceneManager();
            IRenderManager renderManager = new RenderManager();
            IDebugManager debugManager = new DebugManager();

            // Setup the asset manager
            assetManager.RootDirectory = "Content";
            assetManager.AddAsset<Texture2D>("ship.png");
            assetManager.AddAsset<Texture2D>("Planet1.png");
            assetManager.AddAsset<Texture2D>("Planet2.png");
            assetManager.AddAsset<Texture2D>("Planet3.png");
            assetManager.AddAsset<Texture2D>("Sentry1.png");
            assetManager.AddAsset<Texture2D>("Sentry2.png");
            assetManager.AddAsset<Texture2D>("Sentry3.png");
            assetManager.AddAsset<Texture2D>("bullet.png");
            assetManager.AddAsset<Texture2D>("outline.png");
            assetManager.AddAsset<Texture2D>("range.png");
            assetManager.AddAsset<Texture2D>("cursor_25.png");
            assetManager.AddAsset<Texture2D>("cursor_50.png");
            assetManager.AddAsset<Texture2D>("cursor_75.png");
            assetManager.AddAsset<Texture2D>("cursor_100.png");
            assetManager.AddAsset<AudioClip>("AttackCommand.wav");
            assetManager.AddAsset<AudioClip>("PlanetConquered.wav");
            assetManager.AddAsset<AudioClip>("PlanetSelect.wav");
            assetManager.AddAsset<AudioClip>("PlanetUpgrade.wav");
            assetManager.AddAsset<AudioClip>("ShipShotDown.wav");
            assetManager.AddAsset<Font>("main_font.spritefont");
            assetManager.AddAsset<Font>("debug.spritefont");

            // Setup the input manager
            inputManager.AddActionBinding("PrimaryAction", "MouseLeft");
            inputManager.AddActionBinding("SecondaryAction", "MouseRight");
            inputManager.AddActionBinding("ToggleFullScreen", "F11");
            inputManager.AddActionBinding("ToggleDebugConsole", "F12");
            inputManager.AddActionBinding("UpgradePlanet", "W");
            inputManager.AddActionBinding("ChangePlanetToColony", "A");
            inputManager.AddActionBinding("ChangePlanetToSentry", "D");

            // Setup the screen manager
            screenManager.AddSupportedResolution(1920, 1080);
            screenManager.AddSupportedResolution(1280, 720);
            screenManager.SetResolution(1);
            screenManager.IsFullScreen = false;

            // Setup the time manager
            timeManager.TimeScale = 1;

            // Setup the scenes that will be usable in the game
            //IScene myScene = SetupScenePrototype(assetManager);
            IScene myScene = SetupScene(assetManager);
            sceneManager.AddScene(myScene);

            // Setup the debug manager
            debugManager.ConsoleFont = assetManager.GetAsset<Font>("debug");

            // Instantiate the engine with the settings created above
            IEngine engine = new Engine(assetManager, inputManager, screenManager, timeManager, sceneManager, renderManager, debugManager);

            // Start MonoGame
            // MonoGame will load the resources and call the engine's update method
            using (var game = new Game1(engine))
                game.Run();
        }
    }
#endif
}
