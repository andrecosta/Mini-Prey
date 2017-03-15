using InputManager;
using KokoEngine;
using Microsoft.Xna.Framework.Input;

namespace MiniPreyGame
{
    class PlayerController : Script
    {
        public float Speed = 250;

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
                dir = -Transform.Up;
            }
            if (Input.IsKeyDown(Keys.S))
            {
                dir = Transform.Up;
            }
            if (Input.IsKeyDown(Keys.A))
            {
                Transform.rotation -= dt*2;
            }
            if (Input.IsKeyDown(Keys.D))
            {
                Transform.rotation += dt*2;
            }

            _rb.AddForce(dir * Speed * dt);
        }
    }
}
