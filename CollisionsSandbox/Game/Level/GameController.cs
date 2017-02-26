using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MiniPrey.Engine;

namespace MiniPrey.Game
{
    class GameController : Component
    {
        public Texture2D DummyTexture { get; set; }
        public float Speed = 200;

        public override void Awake() 
        {
            GameObject player = new GameObject();

            var sr = player.AddComponent<SpriteRenderer>();
            Sprite sprite = new Sprite();
            sprite.texture = DummyTexture;
            sr.sprite = sprite;
            sr.color = Color.Red;

            player.AddComponent<Rigidbody>();
            player.AddComponent<PlayerController>();
        }

        public override void Start()
        {
        }

        public override void Update(float dt)
        {
            Vector3 dir = Vector3.Zero;

            if (Input.IsKeyDown(Keys.W))
            {
                dir.Y = -1;
            }
            if (Input.IsKeyDown(Keys.A))
            {
                dir.X = -1;
            }
            if (Input.IsKeyDown(Keys.S))
            {
                dir.Y = 1;
            }
            if (Input.IsKeyDown(Keys.D))
            {
                dir.X = 1;
            }

            transform.Translate(dir * Speed * dt);
        }
    }
}
