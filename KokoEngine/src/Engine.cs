/*using System;
using Microsoft.Xna.Framework;

namespace KokoEngine
{
    class Game1 : Game
    {
        private readonly Action _initCallback;
        private readonly Action<float> _updateCallback;
        private readonly Action<float> _renderCallback;

        internal Game1(Action init, Action<float> update, Action<float> render)
        {
            _initCallback = init;
            _updateCallback = update;
            _renderCallback = render;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _initCallback();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _updateCallback((float) gameTime.ElapsedGameTime.TotalSeconds);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _renderCallback((float) gameTime.ElapsedGameTime.TotalSeconds);
        }
    }

    public class Engine
    {
        private IRenderSystem _renderSystem;
        private SceneManager _sceneManager;
        private AssetManager _assetManager;
        private Game1 _game;

        public Engine()
        {
            _sceneManager = new SceneManager();
            _assetManager = new AssetManager();

            _game = new Game1(OnInit, OnUpdate, OnRender);
            _renderSystem = new MonoGameRenderSystem();
            _game.Run();
        }

        private void OnInit()
        {
           
        }

        private void OnUpdate(float dt)
        {

        }

        private void OnRender(float dt)
        {
            _renderSystem.Draw(dt);
        }
    }
}
*/