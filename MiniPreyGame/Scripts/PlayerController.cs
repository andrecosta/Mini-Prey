using InputManager;
using KokoEngine;
using Microsoft.Xna.Framework.Input;

namespace MiniPreyGame
{
    class PlayerController : Script
    {
        public float Speed = 6;

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
                Debug.Log("Moving UP");
            }
            if (Input.IsKeyDown(Keys.S))
            {
                dir = Transform.Up;
                Debug.Log("Moving DOWN");
            }
            if (Input.IsKeyDown(Keys.A))
            {
                Transform.Rotation -= dt*2;
                Debug.Log("Moving LEFT");
            }
            if (Input.IsKeyDown(Keys.D))
            {
                Transform.Rotation += dt*2;
                Debug.Log("Moving RIGHT");
            }

            _rb.AddForce(dir * Speed);

            Debug.Track(GameObject);
        }
    }
}
