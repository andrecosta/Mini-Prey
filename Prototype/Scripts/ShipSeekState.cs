using KokoEngine;

class ShipSeekState : State
{
    private Ship _ship;

    public override void OnLoad()
    {
        _ship = Agent.GetComponent<Ship>();
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        _ship.Seek.target = _ship.Target.Transform;
        _ship.Seek.Enabled = true;

        _ship.GetComponent<Vehicle>().MaxSpeed = 90;

        // Play seek animation
        //_ship.Animator.Play("seek");
        //_ship.Transform.Scale = Vector3.One * 0.7f;

        // Play seek sound
        //_ship.AudioSource.Play(_ship.SeekSound);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        /*if ((_ship.Target.Position - _ship.Transform.Position).SqrMagnitude < 10 * 10)
        {
            FSM.SetState<BoidFleeState>();
        }*/
    }

    public override void OnExitState()
    {
        base.OnExitState();
        _ship.Seek.target = null;
    }
}
