using System;
using System.Collections.Generic;
using InputManager;
using KokoEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
            
            // Create initial scene
            IScene levelScene = _sceneManager.CreateScene("level");

            // Load assets from the Asset Manager
            Texture2D playerTexture = _assetManager.GetAsset<Texture2D>("player");
            Texture2D boidTexture = _assetManager.GetAsset<Texture2D>("boid");
            Texture2D boidSeekTexture = _assetManager.GetAsset<Texture2D>("boidSeek");
            AudioClip boidSeekSound = _assetManager.GetAsset<AudioClip>("boidSeekSound");
            AudioClip boidFleeSound = _assetManager.GetAsset<AudioClip>("boidFleeSound");

            // Create the player GameObject
            IGameObject player = new GameObject();
            {
                // Add some components to the player GameObject
                var sr = player.AddComponent<SpriteRenderer>();
                //var a  = player.AddComponent<Animator>();
                var rb = player.AddComponent<Rigidbody>();
                var cc = player.AddComponent<BoxCollider>();
                var pc = player.AddComponent<PlayerController>();
                var v  = player.AddComponent<Vehicle>();

                // Set the scale of the player's Transform component
                player.Transform.Scale = new Vector3(0.05f, 0.05f, 0.05f);

                // Create a sprite based on the player texture and store it on the player's SpriteRenderer component
                var sprite = new Sprite(playerTexture);
                sr.sprite = sprite;

                // Create an animation clip based on the player sprite and set its properties
                //var animationClip = new AnimationClip(sprite);
                //animationClip.NumFrames = 3;

                // Add the created animation to the Animator component
                //a.AddAnimation("seek", animationClip);

                // Set the BoxCollider component's bounds based on the player texture's dimensions
                cc.Width = sprite.Texture.Height;
                cc.Height = sprite.Texture.Height;

                // Set the Vehicle component's maximum speed
                v.MaxSpeed = 200;

                // Add the player GameObject to the scene
                levelScene.AddGameObject(player);

                // Add the player object to the list of objects being tracked by the Debug console
                Debug.Track(player);
            }

            // Create 50 NPC boids
            for (int i = 0; i < 50; i++)
            {
                // Create the boid GameObject
                var boid = new GameObject();

                // Add some components to the boid GameObject
                var sr = boid.AddComponent<SpriteRenderer>();
                var  a = boid.AddComponent<Animator>();
                var au = boid.AddComponent<AudioSource>();
                var rb = boid.AddComponent<Rigidbody>();
                var cc = boid.AddComponent<BoxCollider>();
                var v  = boid.AddComponent<Vehicle>();
                var seek = boid.AddComponent<Seek>();
                var flee = boid.AddComponent<Flee>();
                var pursuit = boid.AddComponent<Pursuit>();
                var fsm = boid.AddComponent<FSM>();
                var b = boid.AddComponent<Boid>();

                boid.Transform.Position = new Vector3(
                    r.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width),
                    r.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));
                boid.Transform.Scale = new Vector3(0.05f, 0.05f, 0.05f);

                // Create a sprite based on the boid texture and store it on the boid's SpriteRenderer component
                var seekSprite = new Sprite(boidSeekTexture);
                sr.sprite = seekSprite;

                // Create an animation clip based on the player sprite, with 3 frames
                var animationClip = new AnimationClip(seekSprite, 3);

                // Add the created animation to the Animator component and play it
                a.AddAnimation("seek", animationClip);
                a.Play("seek", 0.3f);
                
                // Set the BoxCollider component's bounds based on the boid texture's dimensions
                cc.Width = seekSprite.Texture.Height;
                cc.Height = seekSprite.Texture.Height;

                // Add the created steering behaviours to the vehicle's behaviour list
                v.Behaviours.Add(seek);
                v.Behaviours.Add(flee);
                v.Behaviours.Add(pursuit);

                // Set the player as the boid's target
                b.Target = player.Transform;
                b.SeekSound = boidSeekSound;
                b.FleeSound = boidFleeSound;

                // Add the boid GameObject to the scene
                levelScene.AddGameObject(boid);

                // Add the boid object to the list of objects being tracked by the Debug console
                Debug.Track(boid);
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

            var boidSeekTexture = Content.Load<Microsoft.Xna.Framework.Graphics.Texture2D>("boidSeek");
            _assetManager.AddAsset("boidSeek", new Texture2D("boidSeek", boidSeekTexture, boidSeekTexture.Width, boidSeekTexture.Height));

            var boidSeekSound = Content.Load<SoundEffect>("seekSound");
            _assetManager.AddAsset("boidSeekSound", new AudioClip(boidSeekSound));

            var boidFleeSound = Content.Load<SoundEffect>("fleeSound");
            _assetManager.AddAsset("boidFleeSound", new AudioClip(boidFleeSound));

            var playerTexture = Content.Load<Microsoft.Xna.Framework.Graphics.Texture2D>("player");
            _assetManager.AddAsset("player", new Texture2D("player", playerTexture, playerTexture.Width, playerTexture.Height));
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

            // Draw the active scene's game objects which contain renderable components
            foreach (var rootGameObject in _sceneManager.GetActiveScene().GetRootGameObjects())
                PlaySounds(rootGameObject);

            base.Update(gameTime);
        }

        void PlaySounds(IGameObject rootGameObject)
        {
            foreach (IComponent component in rootGameObject.GetComponents())
            {
                AudioSource au = component as AudioSource;

                // Play MonoGame sound effect
                if (au?.AudioClip?.RawData != null)
                {
                    var soundEffect = au.AudioClip.RawData as SoundEffect;
                    soundEffect?.Play(au.Volume, au.Pitch, au.Pan);
                    au.AudioClip = null;
                }
            }
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
                if (sr == null) continue;

                // Get texture
                Texture2D t = _assetManager.GetAsset<Texture2D>(sr.sprite.Texture.Name);

                // Convert engine data types to MonoGame for use in the Draw call
                var texture = t.RawData as Microsoft.Xna.Framework.Graphics.Texture2D;
                var color = new Microsoft.Xna.Framework.Color(sr.color.R, sr.color.G, sr.color.B);
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
    }
}
