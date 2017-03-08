namespace MiniEngine
{
    class Rigidbody : Component
    {
        static readonly float Damping = 0.98f;
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
            Transform.Translate(velocity * dt);

            acceleration = Gravity;
        }

        public void AddForce(Vector3 force)
        {
            this.force = force;
        }
    }
}
