using InputManager;
using KokoEngine;
using Microsoft.Xna.Framework.Input;

namespace MiniPreyGame
{
    class PlayerController : Behaviour
    {
        public float Speed { get; set; } = 3;

        private Rigidbody _rb;

        protected override void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        protected override void Update(float dt)
        {
            Vector3 dir = Vector3.Zero;

            if (Input.IsKeyDown(Keys.W))
                dir = -Vector3.Up;

            if (Input.IsKeyDown(Keys.S))
                dir = Vector3.Up;

            if (Input.IsKeyDown(Keys.A))
                dir += -Vector3.Right;

            if (Input.IsKeyDown(Keys.D))
                dir += Vector3.Right;

            _rb.AddForce(dir * Speed);

            // Continuous rotation
            Transform.Rotation += dt;
        }
    }
}
