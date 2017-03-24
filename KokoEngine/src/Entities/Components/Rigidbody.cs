using System.Collections.Generic;

namespace KokoEngine
{
    public class Rigidbody : Component
    {
        public static List<Rigidbody> All = new List<Rigidbody>();
        static readonly float Damping = 0.97f;
        static readonly Vector3 Gravity = new Vector3(0, 0, 0);

        public float mass { get; set; }
        public Vector3 velocity { get; set; }
        public Vector3 acceleration { get; set; }
        //Restitution
        //Energy 
        //Vector2 Collision normal

        private List<Vector3> _forces = new List<Vector3>();

        public Rigidbody()
        {
            mass = 1;
            All.Add(this);
        }

        internal override void Update(float dt)
        {
            foreach (Vector3 force in _forces)
                acceleration += force * (1/mass);

            velocity *= Damping;
            velocity += acceleration;
            Transform.Translate(velocity * dt);

            acceleration = Gravity;
            _forces.Clear();
        }

        public void AddForce(Vector3 force)
        {
            if (Vector3.Magnitude(force) > 0)
                _forces.Add(force);
        }
    }
}
