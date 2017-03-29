using System;
using KokoEngine;

namespace MiniPreyGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Instantiate the managers
            ISceneManager sceneManager = new SceneManager();
            IAssetManager assetManager = new AssetManager();

            using (var game = new Game1(sceneManager, assetManager))
                game.Run();
        }
    }
#endif
}
