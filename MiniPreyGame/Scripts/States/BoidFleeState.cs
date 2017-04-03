using KokoEngine;

namespace MiniPreyGame
{
    class BoidFleeState : State
    {
        private Boid _boid;

        public override void OnLoad()
        {
            _boid = Agent.GetComponent<Boid>();
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("FLEE STATE");

            _boid.Seek.Enabled = false;
            _boid.Pursuit.Enabled = false;

            _boid.Flee.target = _boid.Target;
            _boid.Flee.Enabled = true;

            //_boid.Vehicle.MaxSpeed = 2;

            // Change color
            _boid.SpriteRenderer.color = Color.White;

            // Play flee animation
            _boid.Animator.Play("flee", 0.15f);
            _boid.Transform.Scale = Vector3.One*0.7f;

            // Play flee sound
            _boid.AudioSource.Play(_boid.FleeSound);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (Vector3.SqrMagnitude(_boid.Target.Position - _boid.Transform.Position) > 300*300)
            {
                FSM.SetState<BoidSeekState>();
            }
        }

        public override void OnExitState()
        {
            base.OnExitState();
            _boid.Flee.target = null;
        }
    }
}
