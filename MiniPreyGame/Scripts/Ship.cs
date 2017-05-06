using System;
using System.Collections;
using System.Collections.Generic;
using KokoEngine;
using Random = KokoEngine.Random;

public class Ship : Behaviour
{
    public GameController GameController;
    public Player Owner;
    public float Speed = 3;

    public Planet Source { get; set; }
    public Planet Target { get; set; }
    public Seek Seek { get; private set; }

    private Vector3 _velocity;
    private float _randDeviation;
    private float _timer;

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
    }

    protected override void Update()
    {
        Transform.Rotation = (float)Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);

        //_timer -= Time.deltaTime;

        //if (_timer < 0)
        //    GetComponent<Animator>().enabled = true;

        /*_velocity = Vector3.Zero;

        if (Target)
        {
            Vector3 dir = (Target.Transform.Position - Transform.Position).Normalized;
            //transform.Translate(dir * Speed * Time.deltaTime);
            _velocity = dir * Speed * Time.DeltaTime;
            Transform.Position += _velocity;
        }*/

        if (Vector3.Distance(Transform.Position, Target.Transform.Position) <= 20)
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
        Source.Population--;
        Destroy(GameObject);
    }

    /*void OnTriggerEnter(Collider other)
    {
        var shot = other.transform.GetComponent<Shot>();
        if (shot)
        {
            Debug.Log("Trigger");
            Kill();
        }
    }*/
}
