using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CollisionsSandbox
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D dummyTexture;
        Vector2 rect1pos;
        Vector2 rect2pos;
        bool colliding;

        // Prototype 2
        Player player;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Components.Add(new Input(this));        // » InputManager Component
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            player = new Player();                      // » Initialize player object

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

            player.Load(Content);                       // » Load player resources
            dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });
            rect1pos.X = 200;
            rect1pos.Y = 200;
            rect2pos.X = 300;
            rect2pos.Y = 300;
            // TODO: use this.Content to load your game content here
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
            // Gets the number of elapsed seconds since the last update (for use in all movement calculations)
            double dt = gameTime.ElapsedGameTime.TotalSeconds;

            player.Update(dt);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Input.IsKeyDown(Keys.W))
                rect1pos.Y += -2;
            if (Input.IsKeyDown(Keys.A))
                rect1pos.X += -2;
            if (Input.IsKeyDown(Keys.S))
                rect1pos.Y += 2;
            if (Input.IsKeyDown(Keys.D))
                rect1pos.X += 2;
            if (Input.IsKeyDown(Keys.Up))
                rect2pos.Y += -2;
            if (Input.IsKeyDown(Keys.Left))
                rect2pos.X += -2;
            if (Input.IsKeyDown(Keys.Down))
                rect2pos.Y += 2;
            if (Input.IsKeyDown(Keys.Right))
                rect2pos.X += 2;

            Vector2 pos1 = new Vector2(rect1pos.X + 50, rect1pos.Y + 30);
            Vector2 pos2 = new Vector2(rect2pos.X + 50, rect2pos.Y + 30);

            colliding = ((Math.Abs(pos1.Y - pos2.Y) < (60 + 60) * 0.5f) && (Math.Abs(pos1.X - pos2.X) < (100 + 100) * 0.5f));


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

            spriteBatch.Draw(dummyTexture, new Rectangle((int)rect1pos.X, (int)rect1pos.Y, 100, 60), Color.MediumPurple);
            spriteBatch.Draw(dummyTexture, new Rectangle((int)rect2pos.X, (int)rect2pos.Y, 100, 60), Color.LightYellow);

            if (colliding)
            {
                spriteBatch.Draw(dummyTexture, new Rectangle((int)rect1pos.X, 10, 100, 3), Color.Red);
                spriteBatch.Draw(dummyTexture, new Rectangle((int)rect2pos.X, 10, 100, 3), Color.DarkRed);
                spriteBatch.Draw(dummyTexture, new Rectangle(10, (int)rect1pos.Y, 3, 60), Color.Red);
                spriteBatch.Draw(dummyTexture, new Rectangle(10, (int)rect2pos.Y, 3, 60), Color.DarkRed);

            }
            else
            {
                spriteBatch.Draw(dummyTexture, new Rectangle((int)rect1pos.X, 10, 100, 3), Color.MediumPurple);
                spriteBatch.Draw(dummyTexture, new Rectangle((int)rect2pos.X, 10, 100, 3), Color.LightYellow);
                spriteBatch.Draw(dummyTexture, new Rectangle(10, (int)rect1pos.Y, 3, 60), Color.MediumPurple);
                spriteBatch.Draw(dummyTexture, new Rectangle(10, (int)rect2pos.Y, 3, 60), Color.LightYellow);
            }

            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
