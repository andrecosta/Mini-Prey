using System;
using KokoEngine;

namespace MiniPreyGame
{
    class Boid : Behaviour
    {
        public float Speed { get; } = 2;
        public ITransform Target { get; set; }
        public AudioClip SeekSound { get; set; }
        public AudioClip FleeSound { get; set; }
        public bool Pursuer { get; set; }

        public IVehicle Vehicle { get; private set; }
        public Seek Seek { get; private set; }
        public Flee Flee { get; private set; }
        public Pursuit Pursuit { get; private set; }
        public ISpriteRenderer SpriteRenderer { get; private set; }
        public IAnimator Animator { get; private set; }
        public IAudioSource AudioSource { get; private set; }

        private IRigidbody _rb;
        private FSM _fsm;

        static Random r = new Random();

        protected override void Awake()
        {
            _rb = GetComponent<IRigidbody>();
            _fsm = GetComponent<FSM>();

            Vehicle = GetComponent<IVehicle>();
            Seek = GetComponent<Seek>();
            Flee = GetComponent<Flee>();
            Pursuit = GetComponent<Pursuit>();

            SpriteRenderer = GetComponent<ISpriteRenderer>();
            Animator = GetComponent<IAnimator>();
            AudioSource = GetComponent<IAudioSource>();

            // Place the boid on a random location on the screen
            Transform.Position = new Vector3(r.Next(0, Screen.Width), r.Next(0, Screen.Height));
        }

        protected override void Start()
        {
            _fsm.LoadState<BoidSeekState>();
            _fsm.LoadState<BoidFleeState>();
            _fsm.LoadState<BoidPursuitState>();

            if (Pursuer)
                _fsm.SetState<BoidPursuitState>();
            else
                _fsm.SetState<BoidSeekState>();
        }

        protected override void Update()
        {
            // Rotate object towards its heading
            Transform.Rotation = (float) Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);


            // OLD CODE FOR FLOCK SEPARATION
            // TODO: Should go into its own steering behaviour

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
