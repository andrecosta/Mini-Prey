﻿using System.Collections.Generic;

namespace KokoEngine
{
    public class Vehicle : Behaviour, IVehicle
    {
        public Vector3 Direction => _direction;
        public Vector3 Position => Transform.Position;
        public Vector3 Velocity => _rb.velocity;
        public float MaxSpeed { get; set; } = 50;
        public List<ISteeringBehaviour> Behaviours { get; } = new List<ISteeringBehaviour>();

        private IRigidbody _rb;
        private Vector3 _direction;

        protected override void Start()
        {
            _rb = GetComponent<IRigidbody>();
            _direction = Transform.Up;
        }

        protected override void Update()
        {
            Vector3 forces = Vector3.Zero;
            foreach (var b in Behaviours)
            {
                if (b.Enabled)
                    forces += b.Calculate(this);
            }

            _rb.AddForce(forces);

            // Normalise velocity
            if (_rb.velocity.SqrMagnitude > 0.01f * 0.01f)
                _direction = _rb.velocity.Normalized;

            // Limit velocity
            if (_rb.velocity.SqrMagnitude > MaxSpeed * MaxSpeed)
                _rb.velocity = _rb.velocity.Normalized * MaxSpeed;
        }
    }
}
