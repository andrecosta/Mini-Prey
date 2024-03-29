﻿using System.Collections.Generic;

namespace KokoEngine
{
    public class Rigidbody : Component, IRigidbody
    {
        public float Damping { get; } = 0.97f;
        public Vector3 Gravity { get; }= new Vector3(0, 0);

        public float mass { get; set; } = 1;
        public Vector3 velocity { get; set; }
        public Vector3 acceleration { get; private set; }
        //Restitution
        //Energy 
        //Vector2 Collision normal

        private List<Vector3> _forces = new List<Vector3>();

        protected override void Update()
        {
            foreach (Vector3 force in _forces)
                acceleration += force * (1/mass);

            velocity *= Damping;
            velocity += acceleration;
            Transform.Translate(velocity * Time.DeltaTime);

            acceleration = Gravity;
            _forces.Clear();
        }

        public void AddForce(Vector3 force)
        {
            if (force.SqrMagnitude > 0)
                _forces.Add(force);
        }
    }
}
