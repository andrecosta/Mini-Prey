using System;
using System.Collections.Generic;
using InputManager;
using KokoEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = KokoEngine.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Texture2D = KokoEngine.Texture2D;
using Vector3 = KokoEngine.Vector3;

namespace MiniPreyGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        // Managers
        private readonly ISceneManager _sceneManager;
        private readonly IAssetManager _assetManager;
        private readonly ICollisionManager _collisionManager;

        public Game1(ISceneManager sceneManager, IAssetManager assetManager)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Add the InputManager Component
            Components.Add(new Input(this));
            Components.Add(new Debug(this));

            // Store the injected dependencies
            _sceneManager = sceneManager;
            _assetManager = assetManager;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            Random r = new Random();
            
            // Setup initial scene
            IScene levelScene = _sceneManager.CreateScene("level");


            var gameController = new GameObject();
            gameController.AddComponent<GameController>();
            levelScene.AddGameObject(gameController);

            // Texture
            Texture2D boidTexture = _assetManager.GetAsset<Texture2D>("boid");

            // Create Player
            IGameObject player = new GameObject();
            player.Transform.Scale = new Vector3(0.05f, 0.05f, 0.05f);
            ISpriteRenderer sr = player.AddComponent<SpriteRenderer>();
            ISprite sprite = new Sprite(boidTexture);
            sr.sprite = sprite;
            player.AddComponent<Rigidbody>();
            var cc = player.AddComponent<BoxCollider>();
            cc.Width = sprite.texture.Height;
            cc.Height = sprite.texture.Height;
            player.AddComponent<PlayerController>();
            levelScene.AddGameObject(player);

            // Create 50 NPCs
            for (int i = 0; i < 50; i++)
            {
                var boid = new GameObject();
                boid.Transform.Position = new Vector3(
                    r.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width),
                    r.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));
                boid.Transform.Scale = new Vector3(0.05f, 0.05f, 0.05f);
                sr = boid.AddComponent<SpriteRenderer>();
                sprite = new Sprite(boidTexture);
                sr.sprite = sprite;
                sr.color = Color.Red;
                boid.AddComponent<Rigidbody>();
                cc = boid.AddComponent<BoxCollider>();
                cc.Width = sprite.texture.Height;
                cc.Height = sprite.texture.Height;
                var b = boid.AddComponent<Boid>();
                b.Target = player;
                levelScene.AddGameObject(boid);
            }

            // Load the scene
            _sceneManager.LoadScene(levelScene);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load and register resources with the Asset Manager
            var dummyTexture = new Microsoft.Xna.Framework.Graphics.Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new[] { Microsoft.Xna.Framework.Color.White });
            _assetManager.AddAsset("dummy", new Texture2D("dummy", dummyTexture, 1, 1));

            var boidTexture = Content.Load<Microsoft.Xna.Framework.Graphics.Texture2D>("boid");
            _assetManager.AddAsset("boid", new Texture2D("boid", boidTexture, boidTexture.Width, boidTexture.Height));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // Gets the number of elapsed seconds since the last update (for use in all movement calculations)
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the active scene's game objects
            _sceneManager.UpdateActiveScene(dt);

            // Perform collision checks
            _collisionManager.CheckCollisions(_sceneManager.GetActiveScene());

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Microsoft.Xna.Framework.Color(20, 20, 20));
            _spriteBatch.Begin();

            // Draw the active scene's game objects which contain renderable components
            foreach (var rootGameObject in _sceneManager.GetActiveScene().GetRootGameObjects())
                DrawGameObjects(rootGameObject, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawGameObjects(IGameObject rootGameObject, SpriteBatch sb)
        {
            foreach (IComponent component in rootGameObject.GetComponents())
            {
                SpriteRenderer sr = component as SpriteRenderer;
                if (sr == null)
                    continue;

                // Big draw
                // TODO: maybe make it smaller?
                Texture2D t = _assetManager.GetAsset<Texture2D>(sr.sprite.texture.Name);
                sb.Draw((Microsoft.Xna.Framework.Graphics.Texture2D) t.RawData,
                    new Rectangle((int) sr.Transform.Position.X, (int) sr.Transform.Position.Y,
                        (int) (t.Width * sr.Transform.Scale.X), (int) (t.Height * sr.Transform.Scale.Y)), null,
                    new Microsoft.Xna.Framework.Color(sr.color.R, sr.color.G, sr.color.B), sr.Transform.Rotation,
                    new Vector2(t.Width / 2f, t.Height / 2f), SpriteEffects.None, 0);
            }

            foreach (var child in rootGameObject.Transform.Children)
                DrawGameObjects(child.GameObject, sb);
        }
    }
}
