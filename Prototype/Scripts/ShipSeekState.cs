using System;
using KokoEngine;

class ShipSeekState : State
{
    private Ship _ship;
    private IRigidbody _rb;

    public override void OnLoad()
    {
        _ship = Agent.GetComponent<Ship>();
        _rb = Agent.GetComponent<Rigidbody>();
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        _ship.Seek.target = _ship.Target.Transform;
        _ship.Seek.Enabled = true;

        _ship.GetComponent<Vehicle>().MaxSpeed = 90;
        _ship.Transform.Rotation = (float)Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _ship.Transform.Rotation = (float)Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);

        // Check if near the target planet
        if (Vector2.Distance(_ship.Transform.Position, _ship.Target.Transform.Position) <= 20)
        {
            _ship.Target.InsertShip(_ship);
            _ship.Destroy();
        }

        // Separation from other ships (kind of flocking)
        foreach (Ship ship in _ship.GameController.Ships)
        {
            Vector2 diff = ship.Transform.Position - _ship.Transform.Position;
            if (diff.Magnitude < 40)
            {
                if (ship.Owner == null)
                    continue;

                _rb.AddForce(-diff.Normalized * 4f);
            }
        }
    }

    public override void OnExitState()
    {
        base.OnExitState();
        _ship.Seek.target = null;
        _ship.Seek.Enabled = false;
    }
}
