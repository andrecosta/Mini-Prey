using System.Collections.Generic;

namespace KokoEngine
{
    public class Rigidbody : Component, IRigidbody
    {
        public static List<Rigidbody> All = new List<Rigidbody>();
        public float Damping { get; } = 0.97f;
        public Vector3 Gravity { get; }= new Vector3(0, 0, 0);

        public float mass { get; set; } = 1;
        public Vector3 velocity { get; private set; }
        public Vector3 acceleration { get; private set; }
        //Restitution
        //Energy 
        //Vector2 Collision normal

        private List<Vector3> _forces = new List<Vector3>();

        public Rigidbody()
        {
            All.Add(this);
        }

        protected override void Update(float dt)
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
