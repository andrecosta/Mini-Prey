using System.Collections.Generic;
using InputManager;
using KokoEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Managers
        private readonly AssetManager _assetManager = new AssetManager();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Add the InputManager Component
            Components.Add(new Input(this));
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

            // Setup initial scene
            Scene levelScene = new Scene("level");

            //Scene level = SceneManager.CreateScene("level");
            SceneManager.SceneMap.Add("level", levelScene);
            SceneManager.LoadScene(0);

            GameObject gameController = new GameObject();
            gameController.AddComponent<GameController>();

            GameObject player = new GameObject();
            player.Transform.scale = new Vector3(0.05f, 0.05f, 0.05f);
            var sr = player.AddComponent<SpriteRenderer>();
            Sprite sprite = new Sprite("boid");
            sr.sprite = sprite;
            player.AddComponent<Rigidbody>();
            player.AddComponent<PlayerController>();

            SceneManager.AwakeScene();
            SceneManager.StartScene();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

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
            SceneManager.UpdateActiveScene(dt);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Microsoft.Xna.Framework.Color(250, 250, 250));
            spriteBatch.Begin();

            // Draw the active scene's game objects which contain renderable components
            foreach (var rootGameObject in SceneManager.GetActiveScene().GetRootGameObjects())
                DrawGameObjects(rootGameObject, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawGameObjects(GameObject rootGameObject, SpriteBatch sb)
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
                    new Rectangle((int) sr.Transform.position.X, (int) sr.Transform.position.Y,
                        (int) (t.Width * sr.Transform.scale.X), (int) (t.Height * sr.Transform.scale.Y)), null,
                    new Microsoft.Xna.Framework.Color(sr.color.R, sr.color.G, sr.color.B), sr.Transform.rotation,
                    new Vector2(t.Width / 2f, t.Height / 2f), SpriteEffects.None, 0);
            }

            foreach (var child in rootGameObject.Transform.Children)
                DrawGameObjects(child.GameObject, sb);
        }
    }
}
