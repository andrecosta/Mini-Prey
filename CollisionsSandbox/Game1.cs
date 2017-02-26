using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MiniPrey.Engine;
using MiniPrey.Engine.SceneManagement;
using MiniPrey.Game;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MiniPrey
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // Monogame-specific
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // UI
        List<MonoObject> monoObjects = new List<MonoObject>();
        Texture2D dummyTexture;

        public Game1()
        {
            // Monogame-specific
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Components.Add(new Input(this));        // » InputManager Component

            // Setup scenes
            Scene levelScene = new Scene();
            // Load first scene
            SceneManager.SceneMap.Add("level", levelScene);
            SceneManager.LoadScene(0);

            GameObject gameController = new GameObject();
            GameController gc = gameController.AddComponent<GameController>();
            gc.DummyTexture = dummyTexture;

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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {}

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Gets the number of elapsed seconds since the last update (for use in all movement calculations)
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the active scene's game objects
            foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
                foreach (Component c in go.GetComponents())
                    c.Update(dt);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(50, 50, 50));

            // TODO: Add your drawing code here
            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.GetTransformation());

            spriteBatch.Begin();

            // Draw all MonoObjects
            foreach (MonoObject mo in monoObjects)
            {
                mo.Draw(spriteBatch);
            }

            // Draw the active scene's game objects which contain renderable components
            foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                if (sr != null && sr.sprite != null)
                    spriteBatch.Draw(dummyTexture, new Rectangle((int)go.transform.position.X, (int)go.transform.position.Y,
                        50, 50), sr.color);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
