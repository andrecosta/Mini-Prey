using KokoEngine;

namespace MiniPreyGame
{
    class BoidPursuitState : State
    {
        private Boid _boid;

        public override void OnLoad()
        {
            _boid = Agent.GetComponent<Boid>();
        }

        public override void OnEnterState()
        {
            base.OnEnterState();

            _boid.Seek.Enabled = false;
            _boid.Flee.Enabled = false;

            _boid.Pursuit.target = _boid.Target.GetComponent<Vehicle>();
            _boid.Pursuit.Enabled = true;

            //_boid.Vehicle.MaxSpeed = 2;

            // Change color
            _boid.SpriteRenderer.Color = Color.White;

            // Play seek animation
            _boid.Animator.Play("defend");
            _boid.Transform.Scale = Vector3.One * 0.7f;

            // Play seek sound
            //_boid.AudioSource.Play(_boid.SeekSound);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if ((_boid.Target.Position - _boid.Transform.Position).SqrMagnitude < 10*10)
            {
                FSM.SetState<BoidFleeState>();
            }
        }

        public override void OnExitState()
        {
            base.OnExitState();
            _boid.Seek.target = null;
        }
    }
}
