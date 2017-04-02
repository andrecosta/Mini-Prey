using System;
using KokoEngine;

namespace MiniPreyGame
{
    class Boid : Behaviour
    {
        public float Speed { get; } = 5;
        public ITransform Target { get; set; }
        public AudioClip SeekSound { get; set; }
        public AudioClip FleeSound { get; set; }

        public Seek Seek { get; private set; }
        public Flee Flee { get; private set; }
        public Pursuit Pursuit { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Animator Animator { get; private set; }
        public AudioSource AudioSource { get; private set; }

        private IRigidbody _rb;
        private FSM _fsm;

        protected override void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _fsm = GetComponent<FSM>();

            Seek = GetComponent<Seek>();
            Flee = GetComponent<Flee>();
            Pursuit = GetComponent<Pursuit>();

            SpriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
            AudioSource = GetComponent<AudioSource>();
        }

        protected override void Start()
        {
            _fsm.LoadState<BoidSeekState>();
            _fsm.LoadState<BoidFleeState>();
            //_fsm.LoadState<BoidPursuitState>();

            _fsm.SetState<BoidSeekState>();
        }

        protected override void Update(float dt)
        {
            //Vector3 dir = (Target.Transform.Position - Transform.Position).Normalized;
            //_rb.AddForce(dir * Speed);

            Transform.Rotation = (float) Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);

            /*foreach (Rigidbody rb in Rigidbody.All)
            {
                Vector3 diff = rb.Transform.Position - Transform.Position;
                if (Vector3.Magnitude(diff) < 50)
                {
                    if (rb.GetComponent<PlayerController>() != null)
                        continue;

                    _rb.AddForce(-diff.Normalized * Speed * 0.2f);
                }
            }*/
        }
    }
}
