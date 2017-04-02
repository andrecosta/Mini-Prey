using InputManager;
using KokoEngine;
using Microsoft.Xna.Framework.Input;

namespace MiniPreyGame
{
    class PlayerController : Behaviour
    {
        public float Speed = 6;

        private Rigidbody _rb;

        protected override void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        protected override void Update(float dt)
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
                dir += -Vector3.Right;
                //Transform.Rotation -= dt*2;
            }
            if (Input.IsKeyDown(Keys.D))
            {
                dir += Vector3.Right;
                //Transform.Rotation += dt*2;
            }

            _rb.AddForce(dir * Speed);

            
        }
    }
}
