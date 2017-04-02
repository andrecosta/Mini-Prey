using System.Collections.Generic;

namespace KokoEngine
{
    public class Vehicle : Behaviour
    {
        public Vector3 Direction => _direction;
        public Vector3 Position => Transform.Position;
        public Vector3 Velocity => _rb.velocity;
        public float MaxSpeed { get; set; } = 100;
        public List<SteeringBehaviour> Behaviours { get; } = new List<SteeringBehaviour>();

        private IRigidbody _rb;
        private Vector3 _direction;

        protected override void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _direction = Transform.Up;
        }

        protected override void Update(float dt)
        {
            Vector3 forces = Vector3.Zero;
            foreach (var b in Behaviours)
            {
                if (b.Enabled)
                    forces += b.Calculate(this);
            }

            _rb.AddForce(forces);

            // Normalise velocity
            if (_rb.velocity.sqrMagnitude > 0.01f * 0.01f)
                _direction = _rb.velocity.Normalized;

            // Limit velocity
            if (_rb.velocity.sqrMagnitude > MaxSpeed * MaxSpeed)
                _rb.velocity = _rb.velocity.Normalized * MaxSpeed;
        }
    }
}
