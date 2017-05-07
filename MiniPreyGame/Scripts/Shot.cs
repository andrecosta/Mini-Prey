using System;
using KokoEngine;

class Shot : Behaviour
{
    public Ship Target { get; set; }

    private IRigidbody _rb;
    private Pursuit _pursuit;

    protected override void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pursuit = GetComponent<Pursuit>();
        _pursuit.target = Target.GetComponent<Vehicle>();
        GetComponent<Vehicle>().MaxSpeed = 130;

        Transform.Rotation = (float)Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);
    }

    protected override void Update()
    {
        Transform.Rotation = (float)Math.Atan2(_rb.velocity.X, -_rb.velocity.Y);

        if (!Target.Enabled)
            Destroy(GameObject);

        if (Vector3.Distance(Transform.Position, Target.Transform.Position) <= 5)
        {
            Target.Kill();
            Destroy(GameObject);
        }
    }
}
