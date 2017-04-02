using KokoEngine;

namespace MiniPreyGame
{
    class BoidSeekState : State
    {
        private Boid _boid;

        public override void OnLoad()
        {
            _boid = Agent.GetComponent<Boid>();
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("SEEK STATE");

            _boid.Flee.Enabled = false;
            _boid.Pursuit.Enabled = false;

            _boid.Seek.target = _boid.Target;
            _boid.Seek.Enabled = true;

            //_boid.Vehicle.MaxSpeed = 2;

            // Change color
            _boid.SpriteRenderer.color = Color.Red;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (Vector3.SqrMagnitude(_boid.Target.Position - _boid.Transform.Position) < 4*4)
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
