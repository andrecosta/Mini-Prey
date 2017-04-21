// Here, only engine interfaces will be used!

using System;
using System.IO;
using KokoEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MiniPreyGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public partial class Game1 : Game
    {
        private readonly Engine _engine;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public Game1(Engine engine)
        {
            // Store the injected engine dependency
            _engine = engine;

            // -----------------------------------------------------------------------------

            _graphics = new GraphicsDeviceManager(this);
            
            // Set resolution
            _graphics.PreferredBackBufferWidth = Screen.Width;
            _graphics.PreferredBackBufferHeight = Screen.Height;

            Content.RootDirectory = "Content";
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

            // Setup the scene and all its objects
            SetupScene();

            _engine.Initialize();
        }

        /*
        void LoadTexture2D(KokoEngine.Texture2D texture)
        {
            if (texture.Name == "dummy") return;
            
            FileStream fileStream = new FileStream("Content/" + texture.Name, FileMode.Open);

            var t = Texture2D.FromStream(GraphicsDevice, fileStream);
            texture.RawData = t;
            texture.Width = t.Width;
            texture.Height = t.Height;

            fileStream.Dispose();
        }

        void LoadSoundEffect(KokoEngine.AudioClip audio)
        {
            FileStream fileStream = new FileStream("Content/" + audio.Name, FileMode.Open);

            var a = SoundEffect.FromStream(fileStream);
            audio.RawData = a;

            fileStream.Dispose();
        }*/

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // LOAD ASSETS
            foreach (var assetEntry in _engine.AssetManager.AssetMap)
            {
                string filename = assetEntry.Key;
                IAsset asset = assetEntry.Value;

                if (asset is KokoEngine.Texture2D)
                {
                    Texture2D texture = Content.Load<Texture2D>(filename);
                    (asset as KokoEngine.Texture2D).SetData(texture, texture.Width, texture.Height);
                }
                else if (asset is KokoEngine.AudioClip)
                {
                    SoundEffect sound = Content.Load<SoundEffect>(filename);
                    (asset as KokoEngine.AudioClip).SetData(sound, sound.Duration);
                }
                else if (asset is KokoEngine.Font)
                {
                    SpriteFont font = Content.Load<SpriteFont>(filename);
                    (asset as KokoEngine.Font).SetData(font);
                }
            }

            // Load dummy texture (1x1) for line and panel drawing
            Texture2D dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            dummyTexture.SetData(new[] { Color.White });
            var t = new KokoEngine.Texture2D();
            t.SetData(dummyTexture, 1, 1);
            _engine.AssetManager.LoadAsset("dummy", t);
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

            // Update the engine's tracked input state
            InputTracking();

            // Update the engine
            _engine.Update(dt);

            base.Update(gameTime);
        }

        void InputTracking()
        {
            // Update key states
            foreach (var key in _engine.InputManager.TrackedKeys)
            {
                Keys k;
                if (!Enum.TryParse(key.Name, true, out k)) continue;

                key.PreviousState = key.CurrentState;
                key.CurrentState = Keyboard.GetState().IsKeyDown(k) ? Key.State.Down : Key.State.Up;
            }

            // Update mouse state
            var mousePos = Mouse.GetState().Position;
            _engine.InputManager.MousePosition = mousePos.ToKokoVector2();
        }

        void PlaySounds(IGameObject rootGameObject)
        {
            /*foreach (IComponent component in rootGameObject.GetComponents())
            {
                IAudioSource au = component as IAudioSource;

                // Play MonoGame sound effect
                if (au?.AudioClip?.RawData != null)
                {
                    var soundEffect = au.AudioClip.RawData as SoundEffect;
                    soundEffect?.Play(au.Volume, au.Pitch, au.Pan);
                    au.AudioClip = null;
                }
            }*/
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(29, 29, 29));
            _spriteBatch.Begin();

            // DRAW ENGINE OBJECTS (SPRITES)
            // Draw the active scene's game objects which contain renderable components
            foreach (var rootGameObject in _engine.SceneManager.GetActiveScene().GetRootGameObjects())
                DrawGameObjects(rootGameObject, _spriteBatch);

            // DRAW DEBUG
            // ...

            // Draw graph edges
            /*List<GraphEdge> edges = _waypointsController.GetComponent<WaypointsController>().graph.edges;
            for (int i = 0; i < edges.Count; i++)
            {
                GraphEdge e = edges[i];
                Waypoint t1 = e.from as Waypoint;
                Waypoint t2 = e.to as Waypoint;
                DrawLine(new Vector2(t1.Transform.Position.X, t1.Transform.Position.Y),
                    new Vector2(t2.Transform.Position.X, t2.Transform.Position.Y), Microsoft.Xna.Framework.Color.DimGray);
            }

            // Draw shortest path
            List<IGraphNode> spEdges = _waypointsController.GetComponent<WaypointsController>().shortestPath;
            for (int i = 0; i < spEdges.Count-1; i++)
            {
                IGraphNode n = spEdges[i];
                Waypoint t1 = n as Waypoint;
                Waypoint t2 = spEdges[i+1] as Waypoint;

                DrawLine(new Vector2(t1.Transform.Position.X, t1.Transform.Position.Y),
                    new Vector2(t2.Transform.Position.X, t2.Transform.Position.Y), Microsoft.Xna.Framework.Color.Red, 5);
            }*/


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawGameObjects(IGameObject rootGameObject, SpriteBatch sb)
        {
            foreach (IComponent component in rootGameObject.GetComponents())
            {
                ISpriteRenderer sr = component as ISpriteRenderer;
                if (sr == null) continue;
                
                // Convert engine data types to MonoGame for use in the Draw call
                Texture2D texture = sr.sprite.Texture.RawData as Texture2D;
                Color color = sr.color.ToMonoColor();
                Rectangle sourceRectangle = new Rectangle(sr.sprite.SourceRect.X, sr.sprite.SourceRect.Y,
                    sr.sprite.SourceRect.Width, sr.sprite.SourceRect.Height);

                // Additional draw parameters
                Rectangle destinationRectangle = new Rectangle((int) sr.Transform.Position.X, (int) sr.Transform.Position.Y,
                    (int) (sourceRectangle.Width * sr.Transform.Scale.X), (int) (sourceRectangle.Height * sr.Transform.Scale.Y));

                Vector2 origin = new Vector2(sourceRectangle.Width / 2f, sourceRectangle.Height / 2f);

                // DRAW IT
                sb.Draw(texture, destinationRectangle, sourceRectangle, color, sr.Transform.Rotation, origin, SpriteEffects.None, 0);
            }

            // Recursive call for all children GameObjects
            foreach (var child in rootGameObject.Transform.Children)
                DrawGameObjects(child.GameObject, sb);
        }

        void DrawLine(Vector2 start, Vector2 end, Color color, int size = 1)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            var t = _engine.AssetManager.GetAsset<KokoEngine.Texture2D>("dummy").RawData as Texture2D;

            _spriteBatch.Draw(t, new Rectangle((int) start.X, (int) start.Y, (int) edge.Length(), size), null,
                color, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
