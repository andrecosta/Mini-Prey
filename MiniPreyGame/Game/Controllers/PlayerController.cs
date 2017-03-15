using InputManager;
using KokoEngine;
using Microsoft.Xna.Framework.Input;

namespace MiniPreyGame
{
    class PlayerController : Script
    {
        public float Speed = 500;

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
            if (Input.IsKeyDown(Keys.Q))
            {
                Transform.rotation -= dt;
            }
            if (Input.IsKeyDown(Keys.E))
            {
                Transform.rotation += dt;
            }

            _rb.AddForce(dir * Speed * dt);
        }
    }
}
