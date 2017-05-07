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

    private FSM _fsm;

    protected override void Awake()
    {
        _fsm = GetComponent<FSM>();
        Seek = GetComponent<Seek>();
    }

    protected override void Start()
    {
        _fsm.LoadState<ShipSeekState>();
        _fsm.LoadState<ShipDestroyedState>();
        _fsm.SetState<ShipSeekState>();
    }

    protected override void OnDisable()
    {
        GameController.Ships.Remove(this);
    }

    public void Kill()
    {
        _fsm.SetState<ShipDestroyedState>();
    }

    public void Destroy()
    {
        GameController.Ships.Remove(this);
        GameController.OnPopulationChange?.Invoke();
        GameObject.SetActive(false);
        Destroy(GameObject);
    }
}
