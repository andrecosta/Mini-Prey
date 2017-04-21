using System.Collections.Generic;
using KokoEngine;

namespace MiniPreyGame
{
    public partial class Game1
    {
        void SetupScene()
        {
            ISceneManager sceneManager = _engine.SceneManager;
            IAssetManager assetManager = _engine.AssetManager;

            // Create scene
            IScene scene = sceneManager.CreateScene("Level 1");

            // Create the player GameObject
            IGameObject player = new GameObject();
            {
                // Add some components to the player GameObject
                var sr = player.AddComponent<SpriteRenderer>();
                var rb = player.AddComponent<Rigidbody>();
                var cc = player.AddComponent<BoxCollider>();
                var pc = player.AddComponent<PlayerController>();
                var v = player.AddComponent<Vehicle>();

                // Set the player's position to the centre of the screen
                player.Transform.Position = new Vector3(Screen.Width / 2f, Screen.Height / 2f);

                // Set the scale of the player's Transform component
                //player.Transform.Scale = new Vector3(0.05f, 0.05f, 0.05f);

                // Create a sprite based on the player texture and store it on the player's SpriteRenderer component
                var playerTexture = assetManager.GetAsset<Texture2D>("player");
                var sprite = new Sprite(playerTexture);
                sr.sprite = sprite;

                // Set the BoxCollider component's bounds based on the player texture's dimensions
                cc.Width = sprite.Texture.Height;
                cc.Height = sprite.Texture.Height;

                // Set the Vehicle component's maximum speed
                v.MaxSpeed = 100;

                // Add the player GameObject to the scene
                scene.AddGameObject(player);

                // Add the player object to the list of objects being tracked by the Debug console
                //Debug.Track(player);
            }

            Stack<IGameObject> boids = new Stack<IGameObject>();
            // Create 20 NPC boids
            for (int i = 0; i < 20; i++)
            {
                // Create the boid GameObject
                var boid = new GameObject();

                // Add some components to the boid GameObject
                var sr = boid.AddComponent<SpriteRenderer>();
                var a = boid.AddComponent<Animator>();
                var au = boid.AddComponent<AudioSource>();
                var rb = boid.AddComponent<Rigidbody>();
                var cc = boid.AddComponent<BoxCollider>();
                var v = boid.AddComponent<Vehicle>();
                var seek = boid.AddComponent<Seek>();
                var flee = boid.AddComponent<Flee>();
                var pursuit = boid.AddComponent<Pursuit>();
                var fsm = boid.AddComponent<FSM>();
                var b = boid.AddComponent<Boid>();
                
                // Create sprites based on the boid textures
                var boidTexture = assetManager.GetAsset<Texture2D>("boid");
                var boidRainbowTexture = assetManager.GetAsset<Texture2D>("boid_rainbow");

                var boidSprite = new Sprite(boidTexture);
                var boidRainbowSprite = new Sprite(boidRainbowTexture);

                // Create animation clips based on the created sprites, with different number of frames
                var seekAnimationClip = new AnimationClip(boidSprite, 1);
                var fleeAnimationClip = new AnimationClip(boidRainbowSprite, 12);
                var defenderAnimationClip = new AnimationClip(boidSprite, 1);

                // Add the created animations to the Animator component
                a.AddAnimation("seek", seekAnimationClip);
                a.AddAnimation("flee", fleeAnimationClip);
                a.AddAnimation("defend", defenderAnimationClip);

                // Set the BoxCollider component's bounds based on the boid texture's dimensions
                cc.Width = boidSprite.Texture.Height;
                cc.Height = boidSprite.Texture.Height;

                // Add the created steering behaviours to the vehicle's behaviour list
                v.Behaviours.Add(seek);
                v.Behaviours.Add(flee);
                v.Behaviours.Add(pursuit);

                // Set the player as the boid's target
                b.Target = player.Transform;
                var boidSeekSound = assetManager.GetAsset<AudioClip>("seekSound");
                var boidFleeSound = assetManager.GetAsset<AudioClip>("fleeSound");
                b.SeekSound = boidSeekSound;
                b.FleeSound = boidFleeSound;

                // Assign half of the boids to seek the player, and another half to pursue other boids
                if (i < 10)
                {
                    b.Target = player.Transform;
                    boids.Push(boid);
                }
                else
                {
                    b.Target = boids.Pop().Transform;
                    b.Pursuer = true;
                }

                // Add the boid GameObject to the scene
                scene.AddGameObject(boid);

                // Add the boid object to the list of objects being tracked by the Debug console
                //Debug.Track(boid);
            }

            // Create the waypoints controller GameObject and add it to the scene
            var waypointsController = new GameObject();
            var wc = waypointsController.AddComponent<WaypointsController>();
            wc.player = player;
            scene.AddGameObject(waypointsController);

            // Create 25 waypoints
            for (int i = 0; i < 25; i++)
            {
                // Create the waypoint GameObject
                var waypoint = new GameObject();

                // Add some components to the boid GameObject
                var sr = waypoint.AddComponent<SpriteRenderer>();
                var a = waypoint.AddComponent<Animator>();
                var au = waypoint.AddComponent<AudioSource>();
                var w = waypoint.AddComponent<Waypoint>();
                
                // Create sprites based on the boid textures
                var waypointTexture = assetManager.GetAsset<Texture2D>("waypoint_red");
                var waypointSprite = new Sprite(waypointTexture);
                sr.sprite = waypointSprite;

                // Create animation clips based on the created sprites, with different number of frames
                var waypointAnimationClip = new AnimationClip(waypointSprite, 6);

                // Add the created animations to the Animator component
                a.AddAnimation("waypoint", waypointAnimationClip);
                a.Play("waypoint", 0.1f);

                // Add the waypoint to the controller
                wc.AddWaypoint(w);

                // Add the boid GameObject to the scene
                scene.AddGameObject(waypoint);
            }
        }
    }
}
