using System;
using System.Collections;
using System.Collections.Generic;
using KokoEngine;
using Random = KokoEngine.Random;

public class Ship : Behaviour
{
    public GameController GameController { get; set; }
    public Player Owner { get; set; }
    public Planet Source { get; set; }
    public Planet Target { get; set; }
    public Seek Seek { get; private set; }
    public bool IsBeingTargeted { get; set; }

    private IRigidbody _rb;
    private FSM _fsm;

    protected override void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _fsm = GetComponent<FSM>();
        Seek = GetComponent<Seek>();
    }

    protected override void Start()
    {
        _fsm.LoadState<ShipSeekState>();
        _fsm.SetState<ShipSeekState>();
        Transform.Rotation = (float)Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);
    }

    protected override void Update()
    {
        Transform.Rotation = (float)Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);

        foreach (Ship ship in GameController.Ships)
        {
            Vector2 diff = ship.Transform.Position - Transform.Position;
            if (diff.Magnitude < 40)
            {
                if (ship.Owner == null)
                    continue;

                _rb.AddForce(-diff.Normalized * 4f);
            }
        }

        if (Vector2.Distance(Transform.Position, Target.Transform.Position) <= 20)
        {
            Target.InsertShip(this);
            Destroy(GameObject);
        }
    }

    protected override void OnDisable()
    {
        GameController.Ships.Remove(this);
    }

    public void Kill()
    {
        GameController.Ships.Remove(this);
        GameController.OnPopulationChange?.Invoke();
        Destroy(GameObject);
    }
}
