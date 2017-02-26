using InputManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniPrey.Engine;

namespace MiniPrey.Game
{
    class PlayerController : Component
    {
        public float Speed = 200;

        private Rigidbody _rb;

        public override void Awake()
        {
            _rb = GetComponent<Rigidbody>();
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

            _rb.AddForce(dir * Speed * dt);
        }
    }
}
