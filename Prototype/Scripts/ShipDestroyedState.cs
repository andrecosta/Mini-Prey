using KokoEngine;

class ShipDestroyedState : State
{
    private Ship _ship;
    private IAnimator _animator;

    public override void OnLoad()
    {
        _ship = Agent.GetComponent<Ship>();
        _animator = Agent.GetComponent<Animator>();
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        _ship.GetComponent<Vehicle>().MaxSpeed = 15;
        _ship.GetComponent<Vehicle>().Behaviours.Clear();
        _animator.Play("explosion", 0.08f);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        // Destroy the ship when the animaiton finishes playing
        if (!_animator.IsPlaying)
            _ship.Destroy();
    }
}
