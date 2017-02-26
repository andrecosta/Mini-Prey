using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPrey.Engine
{
    class Rigidbody : Component
    {
        static readonly float Damping = 0.9f;
        static readonly Vector3 Gravity = new Vector3(0, 0, 0);

        public float mass { get; set; }
        private Vector3 force { get; set; }
        public Vector3 velocity { get; set; }
        public Vector3 acceleration { get; set; }
        //Restitution
        //Energy 
        //Vector2 Collision normal

        public Rigidbody()
        {
            mass = 1;
        }

        public override void Update(float dt)
        {
            acceleration += force * (1/mass);
            velocity *= Damping;
            velocity += acceleration;
            transform.Translate(velocity * dt);

            acceleration = Gravity;
        }

        public void AddForce(Vector3 force)
        {
            this.force = force;
        }
    }
}
