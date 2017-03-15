using System.Collections.Generic;
using InputManager;
using KokoEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = KokoEngine.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;
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

        Texture2D dummyTexture;
        Dictionary<string, Texture2D> textureMap = new Dictionary<string, Texture2D>();

        public Game1()
        {
            // Monogame-specific
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Components.Add(new Input(this));            // » InputManager Component

            // Setup scene 1
            Scene levelScene = new Scene();
            SceneManager.SceneMap.Add("level", levelScene);
            SceneManager.LoadScene(0);

            GameObject gameController = new GameObject();
            gameController.AddComponent<GameController>();

            GameObject player = new GameObject();
            player.Transform.scale = new Vector3(0.05f, 0.05f, 0.05f);
            var sr = player.AddComponent<SpriteRenderer>();
            Sprite sprite = new Sprite();
            sprite.texture = "boid";
            sr.sprite = sprite;
            sr.color = Color.White;

            player.AddComponent<Rigidbody>();
            player.AddComponent<PlayerController>();

            SceneManager.AwakeScene();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SceneManager.StartScene();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create a dummy texture
            dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new[] { Microsoft.Xna.Framework.Color.White });
            textureMap.Add("dummy", dummyTexture);
            textureMap.Add("boid", Content.Load<Texture2D>("boid"));
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
            foreach (Component component in rootGameObject.GetComponents())
            {
                SpriteRenderer sr = component as SpriteRenderer;
                if (sr == null)
                    continue;

                // Big draw
                sb.Draw(textureMap[sr.sprite.texture],
                    new Rectangle((int) sr.Transform.position.X, (int) sr.Transform.position.Y,
                        (int) (textureMap[sr.sprite.texture].Width * sr.Transform.scale.X),
                        (int) (textureMap[sr.sprite.texture].Height * sr.Transform.scale.Y)), null,
                    new Microsoft.Xna.Framework.Color(sr.color.R, sr.color.G, sr.color.B), sr.Transform.rotation,
                    new Vector2(textureMap[sr.sprite.texture].Width / 2f, textureMap[sr.sprite.texture].Height / 2f),
                    SpriteEffects.None, 0);
            }

            foreach (var child in rootGameObject.Transform.Children)
                DrawGameObjects(child.GameObject, sb);
        }
    }
}
