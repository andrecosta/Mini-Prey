// MonoGame main purpose is to READ/ACCESS the engine's state and REACT to it, not SET/CHANGE things inside it

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Color = Microsoft.Xna.Framework.Color;
using KokoEngine;

namespace MiniPreyGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public partial class Game1 : Game
    {
        private readonly IEngine _engine;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _dummyTexture;

        public Game1(IEngine engine)
        {
            // Store the injected engine reference
            _engine = engine;

            // Setup the engine
            _engine.Setup(SetupCallback);
        }

        #region MonoGame Methods

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            _engine.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load dummy texture (1x1) for line and panel drawing
            _dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
            _dummyTexture.SetData(new[] { Color.White });
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
            _engine.Update((float) gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(29, 29, 29));
            _spriteBatch.Begin();

            _engine.Render();

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

        #region Hooks

        void SetupCallback(IScreenManager screenManager, IAssetManager assetManager, IInputManager inputManager, IRenderManager renderManager)
        {
            // Setup specific manager callbacks
            inputManager.GetUpdatedKeyState += GetKeyDownState;
            inputManager.GetUpdatedMouseState += GetMousePosition;
            assetManager.LoadTextureHandler += LoadTexture;
            assetManager.LoadAudioClipHandler += LoadSoundEffect;
            assetManager.LoadFontHandler += LoadSpriteFont;
            renderManager.RenderSpriteHandler += DrawSprite;
            //renderManager.RenderLineHandler += DrawLine;
            //renderManager.RenderRectangleHandler += DrawRectangle;

            // ------ MonoGame options ------

            // Set graphics options
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = screenManager.CurrentResolution.Width;
            _graphics.PreferredBackBufferHeight = screenManager.CurrentResolution.Height;
            _graphics.IsFullScreen = screenManager.IsFullscreen;

            // Set content root directory
            Content.RootDirectory = assetManager.RootDirectory;
        }

        #endregion

        void LoadTexture(KokoEngine.Texture2D asset)
        {
            Texture2D texture = Content.Load<Texture2D>(asset.Name);
            asset.SetData(texture, texture.Width, texture.Height);
        }

        void LoadSoundEffect(KokoEngine.AudioClip asset)
        {
            SoundEffect sound = Content.Load<SoundEffect>(asset.Name);
            asset.SetData(sound, sound.Duration);
        }

        void LoadSpriteFont(KokoEngine.Font asset)
        {
            SpriteFont font = Content.Load<SpriteFont>(asset.Name);
            asset.SetData(font);
        }

        void DrawSprite(ISpriteRenderer sr)
        {
            // Parameters of draw call
            Texture2D texture = sr.Sprite.Texture.ToMonoTexture2D() ?? _dummyTexture; // If no texture exists, use dummy texture
            Rectangle sourceRectangle = sr.Sprite.SourceRect.ToMonoRectangle();
            Rectangle destinationRectangle = new Rectangle((int) sr.Transform.Position.X, (int) sr.Transform.Position.Y,
                (int) (sourceRectangle.Width * sr.Transform.Scale.X),
                (int) (sourceRectangle.Height * sr.Transform.Scale.Y));
            Color color = sr.Color.ToMonoColor();
            float rotation = sr.Transform.Rotation;
            Vector2 origin = new Vector2(sourceRectangle.Width / 2f, sourceRectangle.Height / 2f);
            float layerDepth = sr.Layer;

            // Draw call
            _spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, rotation, origin, SpriteEffects.None, layerDepth);
        }

        /*void RenderCallback(ISceneManager sceneManager)
        {
            

            

            // Draw graph edges
            /List<GraphEdge> edges = _waypointsController.GetComponent<WaypointsController>().graph.edges;
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
            }

        }*/

        /*void DrawLine(Vector2 start, Vector2 end, Color color, int size = 1)
        {
            Vector2 edge = end - start;
            float angle = (float) Math.Atan2(edge.Y, edge.X);

            var t = _engine.AssetManager.GetAsset<KokoEngine.Texture2D>("dummy").RawData as Texture2D;

            _spriteBatch.Draw(t, new Rectangle((int) start.X, (int) start.Y, (int) edge.Length(), size), null,
                color, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }*/


        private bool GetKeyDownState(string keyName)
        {
            Keys k;
            if (Enum.TryParse(keyName, true, out k))
                return Keyboard.GetState().IsKeyDown(k);

            return false;
        }

        private KokoEngine.Vector2 GetMousePosition()
        {
            return Mouse.GetState().Position.ToKokoVector2();
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
    }
}